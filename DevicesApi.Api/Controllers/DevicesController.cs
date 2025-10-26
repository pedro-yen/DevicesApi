using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Common.Exceptions;
using DevicesApi.Common.Utils;
using DevicesApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevicesApi.Api.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceBusinessManager _deviceManager;

        public DevicesController(IDeviceBusinessManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        /// <summary>
        /// Retrieves a device by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the device</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetById(Guid id)
        {
            var device = await _deviceManager.GetByIdAsync(id);

            return Ok(device);
        }

        /// <summary>
        /// Retrieves devices with optional filters 
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<KeysetPagedResult<Device>>> GetAll([FromQuery] DeviceFilterDto filter)
        {
            var devices = await _deviceManager.GetAllAsync(filter);
            return Ok(devices);
        }

        /// <summary>
        /// Creates a new device
        /// </summary>
        /// <param name="dto">Create new device request</param>
        [HttpPost]
        public async Task<ActionResult<Device>> Create(DeviceCreateDto dto)
        {
            // FluentValidation will automatically validate this
            var device = new Device
            {
                Name = dto.Name,
                Brand = dto.Brand,
                State = dto.State
            };

            var created = await _deviceManager.CreateAsync(device);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing device
        /// </summary>
        /// <param name="id">The unique identifier of the device</param>
        /// <param name="dto">Update device request</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<Device>> Update(Guid id, DeviceUpdateDto dto)
        {
            var result = await _deviceManager.PartialUpdateAsync(id, dto);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a device by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the device</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deviceManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
