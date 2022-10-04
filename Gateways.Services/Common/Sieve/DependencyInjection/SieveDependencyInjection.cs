using LeapPro.Services.Common.Sieve;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace Gateways.Services.Common.Sieve.DependencyInjection
{
    /// <summary>
    /// Sieve dependecy injection extensions
    /// </summary>
    public static class SieveDependencyInjection
    {
        /// <summary>
        /// Filtering, Sorting, Expand, Pagination support using Sieve
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Configuration to bind againt the SieveOptions object</param>
        public static void AddSieve(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SieveOptions>(configuration);
            services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        }
    }
}
