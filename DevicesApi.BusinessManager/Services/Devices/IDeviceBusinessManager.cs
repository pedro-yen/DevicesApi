using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
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
        /// Retrieves all devices.
        /// </summary>
        Task<IEnumerable<Device>> GetAllAsync();

        /// <summary>
        /// Retrieves devices filtered by brand.
        /// </summary>
        /// <param name="brand">The brand name to filter by.</param>
        Task<IEnumerable<Device>> GetByBrandAsync(string brand);

        /// <summary>
        /// Retrieves devices filtered by their operational state.
        /// </summary>
        /// <param name="state">
        /// The state of the device:
        /// - <c>Active</c>: Available and ready for use.
        /// - <c>InUse</c>: Currently in use or assigned.
        /// - <c>Inactive</c>: Not active, possibly decommissioned or offline.
        /// </param>
        Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);

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
