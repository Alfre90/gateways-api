using Gateways.Core.Entities;
using Sieve.Services;

namespace Gateways.Services.Common.Sieve.Configurations
{
    public class DeviceSieveConfiguration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Device>(x => x.GatewayId)
                .CanFilter();
            mapper.Property<Device>(x => x.Vendor)
                .CanFilter();
        }
    }
}
