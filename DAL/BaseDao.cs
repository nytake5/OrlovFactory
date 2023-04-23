namespace DAL;

public class BaseDao : IAsyncDisposable, IDisposable
{
    protected readonly FactoryContext DbContext;
    protected BaseDao(FactoryContext dbContext)
    {
        DbContext = dbContext;
    }
    public void Dispose()
    {
        DbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}
