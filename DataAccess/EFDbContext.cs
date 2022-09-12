using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EFDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public EFDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}