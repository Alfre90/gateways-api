using Gateways.Core.Entities;
using Gateways.Services.Common.Models;

namespace Gateways.Services.Devices.Models
{
    /// <summary>
    /// Update Device Dto
    /// </summary>
    public class UpdateDeviceDto : BaseUpdateDeviceDto
    {
        ///<inheritdoc cref="Device.GatewaySerialNumber"/>
        public int GatewayId { get; set; }

        ///<inheritdoc cref="Device.Vendor"/>
        public string? Vendor { get; set; }

        ///<inheritdoc cref="Device.Status"/>
        public string? Status { get; set; }
    }
}
