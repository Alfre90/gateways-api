using Gateways.Services.Common.Models;
using Gateways.Core.Entities;

namespace Gateways.Services.Gateways.Models
{

    /// <summary>
    /// Update Gateway Dto
    /// </summary>
    public class UpdateGatewayDto : BaseUpdateDto
    {
        ///<inheritdoc cref="Gateway.SerialNumber"/>
        public string? SerialNumber { get; set; }

        ///<inheritdoc cref="Gateway.Name"/>
        public string? Name { get; set; }

        ///<inheritdoc cref="Gateway.IPv4"/>
        public string? IPv4 { get; set; }
    }
}
