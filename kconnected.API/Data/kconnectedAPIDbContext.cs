using System;
using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

#nullable disable
namespace kconnected.API.Data
{
    public class kconnectedAPIDbContext : DbContext
    {
        public DbSet<User> Users {get;set;}

        public DbSet<Skill> Skills {get;set;}
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=tcp:becerekh.database.windows.net,1433;Initial Catalog=kconnectedAPIDB;Persist Security Info=False;User ID=becerekhsa;Password=Becereksa0509971997;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseInMemoryDatabase("kconnectedInMemoryDb");
            optionsBuilder.LogTo(Log.Information);
            
            
        }
           

    }
}
#nullable enable