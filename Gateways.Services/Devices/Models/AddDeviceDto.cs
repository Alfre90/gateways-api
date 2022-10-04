using Gateways.Core.Entities;
using Gateways.Services.Common.Models;

namespace Gateways.Services.Devices.Models
{
    /// <summary>
    /// Add Device Dto
    /// </summary>
    public class AddDeviceDto : BaseDto
    {
        ///<inheritdoc cref="Device.GatewayId"/>
        public int GatewayId { get; set; }

        ///<inheritdoc cref="Device.Vendor"/>
        public string? Vendor { get; set; }

        ///<inheritdoc cref="Device.Status"/>
        public bool Status { get; set; }
    }
}
