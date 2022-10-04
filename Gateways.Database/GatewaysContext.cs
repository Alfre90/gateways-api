
using Gateways.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gateways.Database
{
    public class GatewaysContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "GatewaysDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasKey(i => i.Uid);
            modelBuilder.Entity<Device>()
                .Property(p => p.Uid)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Gateway>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Gateway>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Gateway>? Gateways { get; set; }

        public DbSet<Device>? Devices { get; set; }
    }
}

