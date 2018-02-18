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
                         opt.Key.Invoke();
                     }, null, opt.Value, Timeout.Infinite));
                 }

                 //获取每日任务
                 foreach (var opt in _options.GetDailyTasks())
                 {
                     TimeSpan nextTime;
                     var time = DateTime.Now - DateTime.Now.Date;
                     if (time > opt.Key)
                     {
                         nextTime = new TimeSpan(24, 0, 0) - (time - opt.Key);
                     }
                     else
                     {
                         nextTime = opt.Key - time;
                     }
                     _timerList.Add(new Timer((s) =>
                     {
                         opt.Value.Invoke();
                     }, null, nextTime, new TimeSpan(24, 0, 0)));
                 }

                 //一个每分钟的定时器统一处理Cron定时任务
                 var now = DateTime.Now;
                 var cronTasks = _options.GetCronTasks().Where(kv => kv.Key.IsMatch(now));
                 _timerList.Add(new Timer((s) =>
                 {
                     foreach (var cron in cronTasks)
                     {
                         cron.Value.Invoke();
                     }
                 }, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 1, 0)));
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
