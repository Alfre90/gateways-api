using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace LeapPro.Services.Common.Sieve
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            // Scan and apply all configurations
            mapper.ApplyConfigurationsFromAssembly(typeof(ApplicationSieveProcessor).Assembly);

            return mapper;
        }
    }
}
