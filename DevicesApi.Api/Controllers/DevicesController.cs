using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Common.Exceptions;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetById(Guid id)
        {
            var device = await _deviceManager.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device {id} not found.");

            return Ok(device);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetAll()
        {
            var devices = await _deviceManager.GetAllAsync();
            return Ok(devices);
        }

        [HttpGet("brand/{brand}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetByBrand(string brand)
        {
            var devices = await _deviceManager.GetByBrandAsync(brand);
            return Ok(devices);
        }

        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetByState(DeviceState state)
        {
            var devices = await _deviceManager.GetByStateAsync(state);
            return Ok(devices);
        }

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

        [HttpPut("{id}")]
        public async Task<ActionResult<Device>> Update(Guid id, DeviceUpdateDto dto)
        {
            var result = await _deviceManager.PartialUpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deviceManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
