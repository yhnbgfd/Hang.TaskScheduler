using System;

namespace Hang.TaskScheduler.Base
{
    internal class StartupTask
    {
        public Action TaskAction { get; set; }
        public int Delay { get; set; }
    }
}
