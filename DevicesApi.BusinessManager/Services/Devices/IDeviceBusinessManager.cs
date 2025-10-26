using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Common.Utils;
using DevicesApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.BusinessManager.Services.Devices
{
    /// <summary>
    /// Defines the contract for business logic operations related to devices.
    /// </summary>
    public interface IDeviceBusinessManager
    {
        /// <summary>
        /// Retrieves a device by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the device.</param>
        Task<Device?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves devices with optional filters 
        /// </summary>
        Task<KeysetPagedResult<Device>> GetAllAsync(DeviceFilterDto filter);

        /// <summary>
        /// Creates a new device.
        /// </summary>
        /// <param name="device">The device entity to create.</param>
        Task<Device> CreateAsync(Device device);

        /// <summary>
        /// Partially updates a device using a DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the device to update.</param>
        /// <param name="dto">The update data transfer object containing changes.</param>
        Task<Device> PartialUpdateAsync(Guid id, DeviceUpdateDto dto);

        /// <summary>
        /// Deletes a device by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the device to delete.</param>
        Task DeleteAsync(Guid id);
    }

}
