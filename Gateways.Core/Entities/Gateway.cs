namespace Gateways.Core.Entities
{
    /// <summary>
    /// Gateway
    /// </summary>
    public class Gateway
    {
        public int Id { get; set; }

        public string? SerialNumber { get; set; }

        public string? Name { get; set; }

        public string? IPv4 { get; set; }

        public virtual ICollection<Device>? Devices { get; set; }
    }
}

