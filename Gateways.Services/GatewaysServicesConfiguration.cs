using System;
using Gateways.Services.Devices;
using Gateways.Services.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Gateways.Services
{
    public static class GatewaysServicesConfiguration
    {
        /// <summary>
        /// CRUD services
        /// </summary>
        /// <param name="services"></param>
        public static void AddGatewaysServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IGatewaysService, GatewaysService>();
        }
    }
}

