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
        public TaskSchedulerService(IServiceProvider services)
        {
            Debug.WriteLine("TaskSchedulerService 构造函数");
            StartTaskSchedulerServer();
        }

        private void StartTaskSchedulerServer()
        {
            Task.Factory.StartNew(() =>
            {
                Timer timer = new Timer((s) =>
                {
                    Debug.WriteLine(DateTime.Now.ToString());
                }, null, 0, 1000);
            }, TaskCreationOptions.LongRunning);
        }
    }
}
