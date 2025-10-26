using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>
        public async Task<Device?> GetByIdAsync(Guid id) =>
            await _context.Devices.FindAsync(id);

        ///<inheritdoc/>
        public async Task<IEnumerable<Device>> GetAllAsync(DeviceFilterDto filter)
        {
            var query = _context.Devices.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Brand))
            {
                query = query.Where( x => x.Brand == filter.Brand );
            }

            if ((filter.State.HasValue))
            {
                query = query.Where(x => x.State == filter.State);
            }

            return await query.ToListAsync();
        }

        ///<inheritdoc/>
        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Device device)
        {
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task<bool> ExistsAsync(Guid id) =>
            await _context.Devices.AnyAsync(d => d.Id == id);
    }
}
