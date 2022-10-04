using Gateways.Core.Entities;
using Gateways.Services.Common.Models;

namespace Gateways.Services.Devices.Models
{
    /// <summary>
    /// Device Dto
    /// </summary>
    public class DeviceDto : BaseDeviceEntityDto
    {
        ///<inheritdoc cref="Device.GatewayId"/>
        public int GatewayId { get; set; }

        ///<inheritdoc cref="Device.Vendor"/>
        public string? Vendor { get; set; }

        ///<inheritdoc cref="Device.Created"/>
        public DateTime Created { get; set; }

        ///<inheritdoc cref="Device.Status"/>
        public bool Status { get; set; }
    }
}
