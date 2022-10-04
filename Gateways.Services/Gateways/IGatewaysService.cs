using Gateways.Services.Common.Sieve.Pagination;
using Gateways.Services.Gateways.Models;
using Sieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Services.Gateways
{
    /// <summary>
    /// Interface for Gateways services
    /// </summary>
    public interface IGatewaysService
    {
        /// <summary>
        /// Get all Gateways
        /// </summary>
        /// <param name="query">Sorting, filtering and pagination</param>
        /// <returns>List paginated of GatewayDto</returns>
        Task<PagedResult<GatewayDto>> GetAll(SieveModel query);

        /// <summary>
        /// Get Gateway by id
        /// </summary>
        /// <param name="id">Gateway id</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>FeeDto</returns>
        Task<GatewayDto> GetById(int id, CancellationToken cancellation);

        /// <summary>
        /// Create new Gateway
        /// </summary>
        /// <param name="dto">Gatway dto</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>GatewayDto</returns>
        Task<GatewayDto> Create(AddGatewayDto dto, CancellationToken cancellation);

        /// <summary>
        /// Update Gateway
        /// </summary>
        /// <param name="dto">Fee dto</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>Task</returns>
        Task Update(UpdateGatewayDto dto, CancellationToken cancellation);

        /// <summary>
        /// Delete Gateway
        /// </summary>
        /// <param name="id">Gateway id</param>
        /// <param name="cancellation">Cancellation token</param>
        /// <returns>Task</returns>
        Task Delete(int id, CancellationToken cancellation);
    }
}
