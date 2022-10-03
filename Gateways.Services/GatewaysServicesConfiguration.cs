using System;
using Microsoft.AspNetCore.Authentication;

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
            services.AddScoped<ICompanyDescriptionService, CompanyDescriptionService>();
            services.AddScoped<IWoResiduesService, WoResiduesService>();
        }
    }
}

