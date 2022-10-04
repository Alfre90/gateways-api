using System;
using FluentValidation;
using Gateways.Services.Devices;
using Gateways.Services.Gateways;
using Gateways.Services.Validators.Entities;
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

            //validators
            services.AddValidatorsFromAssemblyContaining<GatewaysValidator>();
        }
    }
}

