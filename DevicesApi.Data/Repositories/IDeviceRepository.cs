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
        /// Retrieves all devices from the data store
        /// </summary>
        Task<IEnumerable<Device>> GetAllAsync();

        /// <summary>
        /// Retrieves devices filtered by brand
        /// </summary>
        /// <param name="brand">The brand name to filter by</param>
        Task<IEnumerable<Device>> GetByBrandAsync(string brand);

        /// <summary>
        /// Retrieves devices filtered by their operational state
        /// </summary>
        /// <param name="state">
        /// The state of the device:
        /// - <c>Active</c>: Available and ready for use
        /// - <c>InUse</c>: Currently in use or assigned
        /// - <c>Inactive</c>: Not active, possibly decommissioned or offline
        /// </param>
        Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);

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
