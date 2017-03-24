﻿using Hang.TaskScheduler;
using Hang.TaskScheduler.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up HangTaskScheduler services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class HangTaskSchedulerIServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHangTaskScheduler(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<TaskSchedulerService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddHangTaskScheduler(this IServiceCollection services, Action<TaskSchedulerOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var builder = services.AddSingleton<TaskSchedulerService>();

            return builder;
        }
    }
}