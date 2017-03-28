# Hang.TaskScheduler
[Nuget](https://www.nuget.org/packages/Hang.TaskScheduler/)  
[Cron维基百科](https://zh.wikipedia.org/wiki/Cron)  
  
```
# Cron格式说明
#  ——分钟（0 - 59）
# |  ——小时（0 - 23）
# | |  ——日（1 - 31）
# | | |  ——月（1 - 12）
# | | | |  ——星期（0 - 6，星期日=0）
# | | | | |
# * * * * * 
```
  
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddHangTaskScheduler(options =>
    {
        options.AddDailyTask(new TimeSpan(2, 15, 0), () => { Debug.WriteLine("Cron1_" + DateTime.Now); });
        options.AddCronTask(new Cron("*/3 * * * *"), () => { Debug.WriteLine("Cron2_" + DateTime.Now); });
    });
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseHangTaskScheduler();
}
```
