using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Common.Models
{
    /// <summary>
    /// Base Entity Dto
    /// </summary>
    public class BaseEntityDto : BaseDto
    {
        /// <summary>
        /// Entity ID
        /// </summary>
        public virtual int Id { get; set; }
    }
}
