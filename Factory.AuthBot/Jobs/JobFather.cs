using BLL.Interfaces;
using Hangfire;

namespace Factory.AuthBot.Jobs;

public static class JobFather
{
    private static IServiceProvider _provider;

    public static TimeSpan NotifyJobCron = TimeSpan.FromHours(1);
    
    public static void Initialize(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static void RunNotifyJob(long chatId)
    {
        var job = _provider.GetRequiredService<NotifyService>() ?? null;
        job.ExecuteAsync(chatId).GetAwaiter().GetResult();
    }
}