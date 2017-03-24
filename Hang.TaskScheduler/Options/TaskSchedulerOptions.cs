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
        private List<KeyValuePair<TimeSpan, Action>> DailyTasksMap { get; } = new List<KeyValuePair<TimeSpan, Action>>();

        /// <summary>
        /// 每日任务
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        public void AddDailyTasks(TimeSpan time, Action action)
        {
            DailyTasksMap.Add(new KeyValuePair<TimeSpan, Action>(time, action));
        }

        public List<KeyValuePair<TimeSpan, Action>> GetDailyTasks()
        {
            return DailyTasksMap;
        }
    }
}
