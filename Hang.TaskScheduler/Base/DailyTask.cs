using System;

namespace Hang.TaskScheduler.Base
{
    /// <summary>
    /// 每日任务
    /// </summary>
    internal class DailyTask
    {
        public TimeSpan TaskTime { get; set; }
        public Action TaskAction { get; set; }
        /// <summary>
        /// 如果程序启动时已超过任务设定的时间，是否还补办执行任务，默认false不补办
        /// </summary>
        public bool Reissue { get; set; }
        /// <summary>
        /// 当Reissue=true时，延迟执行的时间
        /// </summary>
        public int Delay { get; set; } = 0;
    }
}
