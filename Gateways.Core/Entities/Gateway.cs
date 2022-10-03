using System;
using System.Net;

namespace Gateways.Core.Entities
{
    /// <summary>
    /// Gateway
    /// </summary>
    public class Gateway
    {
        public string SerialNumber { get; set; } = "";

        public string? Name { get; set; }

        public string? IPv4 { get; set; }

        public virtual ICollection<Device>? Devices { get; set; }
    }
}

