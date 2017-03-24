using System;
using System.Collections.Generic;

namespace Hang.TaskScheduler.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskSchedulerOptions
    {
        /// <summary>
        /// 每日任务列表
        /// </summary>
        private List<KeyValuePair<TimeSpan, Action>> DailyTasksMap { get; } = new List<KeyValuePair<TimeSpan, Action>>();

        /// <summary>
        /// 每日任务
        /// </summary>
        /// <param name="time">每天执行任务的时间</param>
        /// <param name="action">要执行的方法</param>
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
