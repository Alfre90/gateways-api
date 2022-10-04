using Gateways.Core.Entities;
using Sieve.Services;

namespace Gateways.Services.Common.Sieve.Configurations
{
    public class GatewayConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Gateway>(x => x.Name)
                .CanSort()
                .CanFilter();
            mapper.Property<Gateway>(x => x.SerialNumber)
                .CanFilter();
            mapper.Property<Gateway>(x => x.IPv4)
                .CanFilter();
        }
    }
}
