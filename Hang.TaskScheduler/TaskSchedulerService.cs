using Hang.TaskScheduler.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Hang.TaskScheduler
{
    public class TaskSchedulerService
    {
        private readonly TaskSchedulerOptions _options;
        private List<Timer> _timerList = new List<Timer>();

        public TaskSchedulerService(IOptions<TaskSchedulerOptions> options)
        {
            _options = options.Value;

            //获取启动任务
            foreach (var opt in _options.GetStartupTasks())
            {
                _timerList.Add(new Timer((s) =>
                {
                    opt.Key.Invoke();
                }, null, opt.Value, Timeout.Infinite));
            }

            //获取每日任务
            foreach (var opt in _options.GetDailyTasks())
            {
                TimeSpan nextTime;
                var now = DateTime.Now - DateTime.Now.Date;
                if (now > opt.Key)
                {
                    nextTime = new TimeSpan(24, 0, 0) - (now - opt.Key);
                }
                else
                {
                    nextTime = opt.Key - now;
                }
                _timerList.Add(new Timer((s) =>
                {
                    opt.Value.Invoke();
                }, null, nextTime, new TimeSpan(24, 0, 0)));
            }

            //一个每分钟的定时器统一处理Cron定时任务
            _timerList.Add(new Timer((s) =>
            {
                var now = DateTime.Now;
                foreach (var cron in _options.GetCronTasks())
                {
                    if (cron.Key.IsMatch(now))
                    {
                        cron.Value.Invoke();
                    }
                }
            }, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 1, 0)));
        }
    }
}
