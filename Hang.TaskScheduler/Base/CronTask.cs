using System;

namespace Hang.TaskScheduler.Base
{
    internal class CronTask
    {
        public Cron TaskCron { get; set; }
        public Action TaskAction { get; set; }
    }
}
