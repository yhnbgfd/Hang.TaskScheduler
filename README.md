# Hang.TaskScheduler
[Nuget](https://www.nuget.org/packages/Hang.TaskScheduler/)  
[Cron wikipedia](https://zh.wikipedia.org/wiki/Cron)  
  
```
# Cron format:
#  ——minute（0 - 59）
# |  ——hour（0 - 23）
# | |  ——dayOfMonth（1 - 31）
# | | |  ——month（1 - 12）
# | | | |  ——dayOfWeek（0 - 6，Sunday=0）
# | | | | |
# * * * * * 
```
  
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddHangTaskScheduler(options =>
    {
        options.AddStartupTask(() => { Debug.WriteLine("Cron1_StartupTaskDelay0" + DateTime.Now);, 0);
        options.AddDailyTask(new TimeSpan(2, 15, 0), () => { Debug.WriteLine("Cron1_" + DateTime.Now); });
        options.AddCronTask(new Cron("*/3 * * * *"), () => { Debug.WriteLine("Cron2_" + DateTime.Now); });
        options.AddCronTask("*/3 * * * *", () => { Debug.WriteLine("Cron2_" + DateTime.Now); });
    });
}

```
