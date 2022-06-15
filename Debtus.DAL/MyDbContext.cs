using Debtus.TestTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debtus.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyDbForApp.db");
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkingShift> WorkingShifts { get; set; }
    }
}
