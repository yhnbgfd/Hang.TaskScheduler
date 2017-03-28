using Hang.TaskScheduler.Base;
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
        private List<KeyValuePair<TimeSpan, Action>> _dailyTasksList = new List<KeyValuePair<TimeSpan, Action>>();
        /// <summary>
        /// Cron任务列表
        /// </summary>
        private List<KeyValuePair<Cron, Action>> _cronTasksList = new List<KeyValuePair<Cron, Action>>();

        /// <summary>
        /// 添加每日任务
        /// </summary>
        /// <param name="time">每天执行任务的时间</param>
        /// <param name="action">要执行的方法</param>
        public void AddDailyTask(TimeSpan time, Action action)
        {
            _dailyTasksList.Add(new KeyValuePair<TimeSpan, Action>(time, action));
        }

        /// <summary>
        /// 获取所有每日任务
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<TimeSpan, Action>> GetDailyTasks()
        {
            return _dailyTasksList;
        }

        /// <summary>
        /// 添加Cron任务
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="action"></param>
        public void AddCronTask(Cron cron, Action action)
        {
            _cronTasksList.Add(new KeyValuePair<Cron, Action>(cron, action));
        }

        /// <summary>
        /// 获取所有Cron任务
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<Cron, Action>> GetCronTasks()
        {
            return _cronTasksList;
        }
    }
}
