using Gateways.Services.Common.Sieve.Pagination;
using Gateways.Services.Devices.Models;
using Sieve.Models;

namespace Gateways.Services.Devices
{
    /// <summary>
    /// Interface for Devices services
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Get all Devices
        /// </summary>
        /// <param name="query">Sorting, filtering and pagination</param>
        /// <returns>List paginated of DeviceDto</returns>
        Task<PagedResult<DeviceDto>> GetAll(SieveModel query);

        /// <summary>
        /// Get Device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>FeeDto</returns>
        Task<DeviceDto> GetById(int id, CancellationToken cancellation);

        /// <summary>
        /// Create new Device
        /// </summary>
        /// <param name="dto">Gatway dto</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>DeviceDto</returns>
        Task<DeviceDto> Create(AddDeviceDto dto, CancellationToken cancellation);

        /// <summary>
        /// Update Device
        /// </summary>
        /// <param name="dto">Fee dto</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>Task</returns>
        Task Update(UpdateDeviceDto dto, CancellationToken cancellation);

        /// <summary>
        /// Delete Device
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>Task</returns>
        Task Delete(int id, CancellationToken cancellation);
    }
}
