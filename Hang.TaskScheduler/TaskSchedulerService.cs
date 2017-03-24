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
        }
    }
}
