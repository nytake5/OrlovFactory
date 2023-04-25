using BLL.Interfaces;
using Hangfire;

namespace Factory.AuthBot.Jobs;

public static class JobFather
{
    private static IServiceProvider _provider;

    public static TimeSpan NotifyJobCron = TimeSpan.FromHours(1);
    public static TimeSpan CleanJobCron = TimeSpan.FromHours(1);
    
    public static void Initialize(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static void RunNotifyJob(long chatId)
    {
        var job = _provider.GetRequiredService<NotifyService>();
        job.ExecuteAsync(chatId).GetAwaiter().GetResult();
    }

    public static void RunCleanJob(long chatId)
    {
        var job = _provider.GetRequiredService<CleanJob>();
        job.Execute(chatId.ToString());
    }
}