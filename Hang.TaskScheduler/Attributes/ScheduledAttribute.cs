using System;
using System.Collections.Generic;
using System.Text;

namespace Hang.TaskScheduler.Attributes
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScheduledAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Cron { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int? FixedRate { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int? FixedDelay { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="fixedRate"></param>
        /// <param name="fixedDelay"></param>
        public ScheduledAttribute(string cron, int? fixedRate = null, int? fixedDelay = null)
        {
            Cron = cron;
            FixedRate = fixedRate;
            FixedDelay = fixedDelay;
        }
    }
}
