using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace kconnected.API.Data
{
    public class kconnectedAPIDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<Skill> Skills => Set<Skill>();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("kconnectedAPIDb");
        }


    }
}