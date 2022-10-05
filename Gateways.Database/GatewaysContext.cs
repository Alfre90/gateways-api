
using Gateways.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gateways.Database
{
    public class GatewaysContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "GatewaysDb");
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(i => i.Uid);

                entity.Property(p => p.Uid)
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.GatewayId);
            });
        }

        public DbSet<Gateway>? Gateways { get; set; }

        public DbSet<Device>? Devices { get; set; }
    }
}

