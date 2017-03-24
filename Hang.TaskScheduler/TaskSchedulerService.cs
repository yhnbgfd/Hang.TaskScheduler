using Hang.TaskScheduler.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hang.TaskScheduler
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskSchedulerService
    {
        private readonly TaskSchedulerOptions _options;

        public TaskSchedulerService(IOptions<TaskSchedulerOptions> options)
        {
            _options = options.Value;

            StartTaskSchedulerServer();

            Debug.WriteLine("TaskSchedulerService 构造函数");
        }

        private void StartTaskSchedulerServer()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var opt in _options.GetDailyTasks())
                {
                    Timer timer = new Timer((s) =>
                    {
                        opt.Value.Invoke();
                        //Debug.WriteLine(DateTime.Now.ToString());
                    }, null, new TimeSpan(0), opt.Key);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
