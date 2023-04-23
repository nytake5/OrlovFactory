using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class FactoryContext : DbContext
    {
        public FactoryContext(
            DbContextOptions<FactoryContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<WorkingShift> WorkingShifts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
