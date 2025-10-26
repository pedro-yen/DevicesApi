using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
        public async Task<IEnumerable<Device>> GetAllAsync(DeviceFilterDto filter, int pageSize, Guid? lastSeenId, DateTime? lastSeenCreatedAt)
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
            if (lastSeenId.HasValue && lastSeenCreatedAt.HasValue)
            {
                query = query.Where(d =>
                    d.CreatedAt > lastSeenCreatedAt.Value ||
                    (d.CreatedAt == lastSeenCreatedAt.Value && d.Id.CompareTo(lastSeenId.Value) > 0));
            }

            return await query.OrderBy(d => d.Id).Take(pageSize).ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<DateTime?> GetCreatedAtAsync(Guid id)
        {
            return await _context.Devices
                .Where(d => d.Id == id)
                .Select(d => d.CreatedAt)
                .FirstOrDefaultAsync();
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
