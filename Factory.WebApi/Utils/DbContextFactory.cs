using DAL;
using Microsoft.EntityFrameworkCore;

namespace Factory.WebApi.Utils;

public class DbContextFactory : IDbContextFactory<FactoryContext>
{
    public FactoryContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<FactoryContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Factory;User Id=postgres;Password=2323;");

        return new FactoryContext(optionsBuilder.Options);
    }
}