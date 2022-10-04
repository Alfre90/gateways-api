using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Common.Models
{
    public class BaseDeviceEntityDto : BaseDto
    {
        /// <summary>
        /// Entity Uid
        /// </summary>
        public virtual int Uid { get; set; }
    }
}
