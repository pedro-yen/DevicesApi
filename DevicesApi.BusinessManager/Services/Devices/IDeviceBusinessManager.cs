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
    public interface IDeviceBusinessManager
    {
        Task<Device?> GetByIdAsync(Guid id);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<IEnumerable<Device>> GetByBrandAsync(string brand);
        Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);
        Task<Device> CreateAsync(Device device);
        Task<Device> PartialUpdateAsync(Guid id, DeviceUpdateDto dto);
        Task<Device> PartialUpdateAsync(Guid id, Action<Device> patch);
        Task DeleteAsync(Guid id);
    }
}
