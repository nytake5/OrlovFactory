using Microsoft.Extensions.Logging;

namespace BLL;

public class BaseLogic
{
    protected readonly ILogger Logger;

    protected BaseLogic(ILogger<BaseLogic> logger)
    {
        Logger = logger;
    }
}