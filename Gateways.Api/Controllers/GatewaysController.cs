using Microsoft.AspNetCore.Authorization;
using Gateways.Services.Gateways;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Gateways.Services.Gateways.Models;

namespace Gateways.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GatewaysController : ControllerBase
    {
        private readonly IGatewaysService _service;
        private readonly ILogger<GatewaysController> _logger;

        /// <summary>
        /// Main constructor
        /// </summary>
        public GatewaysController(IGatewaysService gatewaysService, ILogger<GatewaysController> logger)
        {
            _service = gatewaysService;
            _logger = logger;
        }

        /// <summary>
        /// Get Gateways
        /// </summary>
        /// <returns>List paginated of GatewaysDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetGateways([FromQuery] SieveModel query)
        {
            var gateways = await _service.GetAll(query);
            return Ok(gateways);
        }

        /// <summary>
        /// Get Gateway by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Dto</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGateway([FromRoute] int id, CancellationToken cancellation = default)
        {
            var gateway = await _service.GetById(id, cancellation);
            return (gateway is null) ? NotFound() : Ok(gateway);
        }

        /// <summary>
        /// Create Gateway
        /// </summary>
        /// <param name="dto">Add dto</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Dto</returns>
        [HttpPost]
        public async Task<IActionResult> PostGateway([FromBody] AddGatewayDto dto, CancellationToken cancellation = default)
        {
            var gateway = await _service.Create(dto, cancellation);
            return CreatedAtAction(nameof(GetGateway), new { id = gateway.Id}, gateway);
        }

        /// <summary>
        /// Update Gateway
        /// </summary>
        /// <param name="id">Id of the entity to update</param>
        /// <param name="dto">Update dto</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway([FromRoute] int id, [FromBody] UpdateGatewayDto dto, CancellationToken cancellation = default)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            await _service.Update(dto, cancellation);
            return NoContent();
        }

        /// <summary>
        /// Delete Gateway
        /// </summary>
        /// <param name="id">Id of the entity to delete</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGateway([FromRoute] int id, CancellationToken cancellation = default)
        {
            await _service.Delete(id, cancellation);
            return NoContent();
        }
    }
}
