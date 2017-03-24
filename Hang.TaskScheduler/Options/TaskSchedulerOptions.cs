using System;
using System.Collections.Generic;
using System.Text;

namespace Hang.TaskScheduler.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskSchedulerOptions
    {
        /// <summary>
        /// Cron任务
        /// </summary>
        /// <param name="cron"></param>
        public void AddCorn(string cron)
        {

        }

        /// <summary>
        /// 每日任务
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        public void AddDailyTasks(TimeSpan time, Action action)
        {

        }
    }
}
