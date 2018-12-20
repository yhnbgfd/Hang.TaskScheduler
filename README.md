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
        options.AddStartupTask(() => { Console.WriteLine($"StartupTask_delay500ms {DateTime.Now:HH:mm:ss.fff}"); }, 500);
        options.AddDailyTask(new TimeSpan(23, 59, 00), () => { Console.WriteLine($"DailyTask {DateTime.Now:HH:mm:ss.fff}"); });
        options.AddDailyTask(new TimeSpan(00, 00, 01), () => { Console.WriteLine($"DailyTasks_Reissue {DateTime.Now:HH:mm:ss.fff}"); }, true);
        //options.AddCronTask(new Cron("*/5 * * * *"), () => { Console.WriteLine($"CronTask {DateTime.Now:HH:mm:ss.fff}"); });
        options.AddCronTask("*/5 * * * *", () => { Console.WriteLine($"CronTask {DateTime.Now:HH:mm:ss.fff}"); });
    });
}

```
