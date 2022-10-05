namespace Gateways.Core.Entities
{
    /// <summary>
    /// Device
    /// </summary>
    public class Device
    {
        public int Uid { get; set; }

        public int GatewayId { get; set; }

        public string? Vendor { get; set; }

        public DateTime Created { get; set; }

        public string? Status { get; set; }

        public virtual Gateway? Gateway { get; set; }
    }
}

