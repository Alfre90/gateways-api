
using Gateways.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Database
{
    public class GatewaysContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "GatewaysDb");
        }

        public DbSet<Gateway>? Gateways { get; set; }

        public DbSet<Device>? Devices { get; set; }
    }
}

