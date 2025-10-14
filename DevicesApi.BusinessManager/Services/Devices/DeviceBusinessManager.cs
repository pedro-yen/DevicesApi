using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Common.Exceptions;
using DevicesApi.Data.Entities;
using DevicesApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.BusinessManager.Services.Devices
{
    public class DeviceBusinessManager : IDeviceBusinessManager
    {
        private readonly IDeviceRepository _repository;

        public DeviceBusinessManager(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Device>> GetByBrandAsync(string brand)
        {
            return await _repository.GetByBrandAsync(brand);
        }

        public async Task<IEnumerable<Device>> GetByStateAsync(DeviceState state)
        {
            return await _repository.GetByStateAsync(state);
        }

        public async Task<Device> CreateAsync(Device device)
        {
            device.Id = Guid.NewGuid();
            device.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(device);
            return device;
        }

        public async Task<Device> PartialUpdateAsync(Guid id, DeviceUpdateDto dto)
        {
            var device = await _repository.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device {id} not found.");

            var originalName = device.Name;
            var originalBrand = device.Brand;


            // Apply changes only if present in dto
            if (dto.Name is not null) device.Name = dto.Name;
            if (dto.Brand is not null) device.Brand = dto.Brand;
            if (dto.State is not null) device.State = dto.State.Value;

            if (device.State == DeviceState.InUse &&
                (device.Name != originalName || device.Brand != originalBrand))
                throw new ValidationException("Cannot update name or brand of a device in use.");

            await _repository.UpdateAsync(device);
            return device;
        }

        public async Task<Device> PartialUpdateAsync(Guid id, Action<Device> patch)
        {
            var device = await _repository.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device {id} not found.");

            var originalName = device.Name;
            var originalBrand = device.Brand;
            var originalCreatedAt = device.CreatedAt;

            patch(device);

            if (device.CreatedAt != originalCreatedAt)
                throw new ValidationException("Creation time cannot be updated.");

            if (device.State == DeviceState.InUse &&
                (device.Name != originalName || device.Brand != originalBrand))
                throw new ValidationException("Cannot update name or brand of a device in use.");

            await _repository.UpdateAsync(device);
            return device;
        }

        public async Task DeleteAsync(Guid id)
        {
            var device = await _repository.GetByIdAsync(id);
            if (device == null)
                throw new NotFoundException($"Device {id} not found.");

            if (device.State == DeviceState.InUse)
                throw new ValidationException("Cannot delete a device that is in use.");

            await _repository.DeleteAsync(device);
        }
    }
}
