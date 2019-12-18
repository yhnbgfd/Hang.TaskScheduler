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
        private List<DailyTask> _dailyTasksList = new List<DailyTask>();
        /// <summary>
        /// Cron任务列表
        /// </summary>
        private List<CronTask> _cronTasksList = new List<CronTask>();
        /// <summary>
        /// 启动运行任务
        /// </summary>
        private List<StartupTask> _startupTasksList = new List<StartupTask>();

        /// <summary>
        /// 添加每日任务
        /// </summary>
        /// <param name="time">每天执行任务的时间</param>
        /// <param name="action">要执行的方法</param>
        /// <param name="reissue">如果程序启动时已超过任务设定的时间，是否还补办执行任务，默认false不补办</param>
        /// <param name="delay">当Reissue=true时，延迟执行的时间</param>
        public void AddDailyTask(TimeSpan time, Action action, bool reissue = false, int delay = 0)
        {
            _dailyTasksList.Add(new DailyTask
            {
                TaskTime = time,
                TaskAction = action,
                Reissue = reissue,
                Delay = delay
            });
        }

        /// <summary>
        /// 获取所有每日任务
        /// </summary>
        /// <returns></returns>
        internal List<DailyTask> GetDailyTasks()
        {
            return _dailyTasksList;
        }

        /// <summary>
        /// 添加Cron任务
        /// </summary>
        /// <param name="cronString">Cron字符串</param>
        /// <param name="action"></param>
        public void AddCronTask(string cronString, Action action)
        {
            AddCronTask(new Cron(cronString), action);
        }

        /// <summary>
        /// 添加Cron任务
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="action"></param>
        public void AddCronTask(Cron cron, Action action)
        {
            _cronTasksList.Add(new CronTask { TaskCron = cron, TaskAction = action });
        }

        /// <summary>
        /// 获取所有Cron任务
        /// </summary>
        /// <returns></returns>
        internal List<CronTask> GetCronTasks()
        {
            return _cronTasksList;
        }

        /// <summary>
        /// 添加启动任务
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delay">延时启动时间，单位毫秒</param>
        public void AddStartupTask(Action action, int delay = 0)
        {
            _startupTasksList.Add(new StartupTask { TaskAction = action, Delay = delay });
        }

        /// <summary>
        /// 获取所有启动任务
        /// </summary>
        /// <returns></returns>
        internal List<StartupTask> GetStartupTasks()
        {
            return _startupTasksList;
        }
    }
}
