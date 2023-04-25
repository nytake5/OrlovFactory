using Microsoft.Extensions.Caching.Distributed;

namespace Factory.AuthBot.Jobs;

public class CleanJob
{
    private readonly IDistributedCache _distributedCache;
    
    public CleanJob(
        IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public void Execute(string key)
    {
        _distributedCache.Remove(key);
    }
}