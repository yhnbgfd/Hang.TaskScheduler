using Hang.TaskScheduler.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HangTaskSchedulerIServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHangTaskScheduler(this IServiceCollection services)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddHangTaskScheduler(this IServiceCollection services, Action<TaskSchedulerOptions> configure)
        {
            return null;
        }
    }
}