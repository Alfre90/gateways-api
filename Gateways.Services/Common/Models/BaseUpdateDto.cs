
namespace Gateways.Services.Common.Models
{

    /// <summary>
    /// Base class for gateways update Dtos
    /// </summary>
    public class BaseUpdateDto : BaseDto
    {
        /// <inheritdoc cref="BaseEntityDto.Id" />
        public int Id { get; set; }
    }
}
