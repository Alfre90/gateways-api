using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Common.Models
{
    /// <summary>
    /// Base class for device update Dtos
    /// </summary>
    public class BaseUpdateDeviceDto : BaseDto
    {
        /// <inheritdoc cref="BaseDeviceEntityDto.Id" />
        public int Uid { get; set; }
    }
}
