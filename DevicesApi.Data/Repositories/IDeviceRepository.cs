using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Data.Repositories
{
    /// <summary>
    /// Defines the data access contract for device persistence operations
    /// </summary>
    public interface IDeviceRepository
    {
        /// <summary>
        /// Retrieves a device by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the device</param>
        Task<Device?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all devices from the data store with optional filters
        /// </summary>
        Task<IEnumerable<Device>> GetAllAsync(DeviceFilterDto filter);

        /// <summary>
        /// Adds a new device to the data store
        /// </summary>
        /// <param name="device">The device entity to add</param>
        Task AddAsync(Device device);

        /// <summary>
        /// Updates an existing device in the data store
        /// </summary>
        /// <param name="device">The device entity with updated values</param>
        Task UpdateAsync(Device device);

        /// <summary>
        /// Deletes a device from the data store
        /// </summary>
        /// <param name="device">The device entity to delete</param>
        Task DeleteAsync(Device device);

        /// <summary>
        /// Checks whether a device exists in the data store
        /// </summary>
        /// <param name="id">The unique identifier of the device</param>
        Task<bool> ExistsAsync(Guid id);
    }

}
