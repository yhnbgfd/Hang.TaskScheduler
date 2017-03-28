using Hang.TaskScheduler.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using WebAppDemo.Schedulers;

namespace WebAppDemo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddHangTaskScheduler(options =>
            {
                options.AddDailyTask(new TimeSpan(2, 14, 0), Class.Do1);
                options.AddDailyTask(new TimeSpan(2, 15, 0), Class.Do3);
                options.AddCronTask(new Cron("* * * * *"), () => { Debug.WriteLine("Cron1_" + DateTime.Now); });
                options.AddCronTask(new Cron("*/3 * * * *"), () => { Debug.WriteLine("Cron2_" + DateTime.Now); });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseHangTaskScheduler();
        }
    }
}
