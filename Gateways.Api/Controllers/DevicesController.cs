using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Gateways.Services.Devices.Models;
using Gateways.Services.Devices;

namespace Gateways.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService _service;
        private readonly ILogger<DevicesController> _logger;

        /// <summary>
        /// Main constructor
        /// </summary>
        public DevicesController(IDevicesService devicesService, ILogger<DevicesController> logger)
        {
            _service = devicesService;
            _logger = logger;
        }

        /// <summary>
        /// Get Devices
        /// </summary>
        /// <returns>List paginated of DevicesDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetDevices([FromQuery] SieveModel query)
        {
            var devices = await _service.GetAll(query);
            return Ok(devices);
        }

        /// <summary>
        /// Get Device by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Dto</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] int id, CancellationToken cancellation = default)
        {
            var device = await _service.GetById(id, cancellation);
            return (device is null) ? NotFound() : Ok(device);
        }

        /// <summary>
        /// Create Device
        /// </summary>
        /// <param name="dto">Add dto</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Dto</returns>
        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] AddDeviceDto dto, CancellationToken cancellation = default)
        {
            var device = await _service.Create(dto, cancellation);
            return CreatedAtAction(nameof(GetDevice), new { id = device.Uid }, device);
        }

        /// <summary>
        /// Update Device
        /// </summary>
        /// <param name="id">Id of the entity to update</param>
        /// <param name="dto">Update dto</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] UpdateDeviceDto dto, CancellationToken cancellation = default)
        {
            if (id != dto.Uid)
            {
                return BadRequest();
            }
            await _service.Update(dto, cancellation);
            return NoContent();
        }

        /// <summary>
        /// Delete Device
        /// </summary>
        /// <param name="id">Id of the entity to delete</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <returns>Task</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id, CancellationToken cancellation = default)
        {
            await _service.Delete(id, cancellation);
            return NoContent();
        }
    }
}
