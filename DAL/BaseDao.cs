namespace DAL;

public class BaseDao : IAsyncDisposable
{
    protected readonly FactoryContext DbContext;
    protected BaseDao(FactoryContext dbContext)
    {
        DbContext = dbContext;
    }
    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}
