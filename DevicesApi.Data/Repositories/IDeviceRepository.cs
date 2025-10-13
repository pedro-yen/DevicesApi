using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Data.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device?> GetByIdAsync(Guid id);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<IEnumerable<Device>> GetByBrandAsync(string brand);
        Task<IEnumerable<Device>> GetByStateAsync(DeviceState state);
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(Device device);
        Task<bool> ExistsAsync(Guid id);
    }
}
