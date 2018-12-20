using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebAppDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddHangTaskScheduler(options =>
            {
                options.AddStartupTask(() => { Console.WriteLine($"StartupTask_delay500ms {DateTime.Now:HH:mm:ss.fff}"); }, 500);
                options.AddDailyTask(new TimeSpan(23, 59, 00), () => { Console.WriteLine($"DailyTask {DateTime.Now:HH:mm:ss.fff}"); });
                options.AddDailyTask(new TimeSpan(00, 00, 01), () => { Console.WriteLine($"DailyTasks_Reissue {DateTime.Now:HH:mm:ss.fff}"); }, true);
                //options.AddCronTask(new Cron("*/5 * * * *"), () => { Console.WriteLine($"CronTask {DateTime.Now:HH:mm:ss.fff}"); });
                options.AddCronTask("*/5 * * * *", () => { Console.WriteLine($"CronTask {DateTime.Now:HH:mm:ss.fff}"); });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
