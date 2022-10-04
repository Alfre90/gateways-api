using Gateways.Core.Entities;
using Gateways.Services.Common.Models;

namespace Gateways.Services.Gateways.Models
{
    /// <summary>
    /// Add Gateway Dto
    /// </summary>
    public class AddGatewayDto : BaseDto
    {
        ///<inheritdoc cref="Gateway.SerialNumber"/>
        public string? SerialNumber { get; set; }

        ///<inheritdoc cref="Gateway.Name"/>
        public string? Name { get; set; }

        ///<inheritdoc cref="Gateway.IPv4"/>
        public string? IPv4 { get; set; }
    }
}
