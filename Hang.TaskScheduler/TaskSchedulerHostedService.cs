using Hang.TaskScheduler.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hang.TaskScheduler
{
    public class TaskSchedulerHostedService : IHostedService
    {
        private CancellationTokenSource _cts;
        private Task _executingTask;

        private readonly TaskSchedulerOptions _options;
        private List<Timer> _timerList = new List<Timer>();

        public TaskSchedulerHostedService(IOptions<TaskSchedulerOptions> options)
        {
            _options = options.Value;
        }

        //IHostedService
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = Task.Run(() =>
             {
                 //获取启动任务
                 foreach (var opt in _options.GetStartupTasks())
                 {
                     _timerList.Add(new Timer((s) =>
                     {
                         opt.TaskAction.Invoke();
                     }, null, opt.Delay, Timeout.Infinite));
                 }

                 //获取每日任务中，标记了补办且执行时间已经过去的任务，在程序启动时执行任务
                 foreach (var opt in _options.GetDailyTasks().Where(s => s.Reissue == true && s.TaskTime < DateTime.Now.TimeOfDay))
                 {
                     _timerList.Add(new Timer((s) =>
                     {
                         opt.TaskAction.Invoke();
                     }, null, opt.Delay, Timeout.Infinite));
                 }

                 //获取每日任务
                 foreach (var opt in _options.GetDailyTasks())
                 {
                     TimeSpan nextTime;
                     var time = DateTime.Now - DateTime.Now.Date;
                     if (time > opt.TaskTime)//当前时间已超过任务执行时间，则下次执行时间在明天
                     {
                         nextTime = TimeSpan.FromDays(1) - (time - opt.TaskTime);
                     }
                     else
                     {
                         nextTime = opt.TaskTime - time;
                     }
                     _timerList.Add(new Timer((s) =>
                     {
                         opt.TaskAction.Invoke();
                     }, null, nextTime, TimeSpan.FromDays(1)));
                 }

                 //一个每分钟的定时器统一处理Cron定时任务
                 var cronTasks = _options.GetCronTasks();
                 _timerList.Add(new Timer((s) =>
                 {
                     var now = DateTime.Now;
                     foreach (var cron in cronTasks.Where(w => w.TaskCron.IsMatch(now)))
                     {
                         cron.TaskAction.Invoke();
                     }
                 }, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)));
             });
            return Task.CompletedTask;
        }

        //IHostedService
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 发送停止信号，以通知我们的后台服务结束执行。
            _cts.Cancel();

            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
