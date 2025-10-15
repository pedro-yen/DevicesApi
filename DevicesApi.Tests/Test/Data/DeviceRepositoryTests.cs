using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data;
using DevicesApi.Data.Entities;
using DevicesApi.Data.Repositories;
using DevicesApi.Tests.Fixtures.Data;
using DevicesApi.Tests.Fixtures.Devices;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Test.Data
{
    public class DeviceRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly DeviceRepository _repository;

        public DeviceRepositoryTests()
        {
            _context = DbContextFactory.CreateInMemoryContext();
            DbSeeder.SeedDevices(_context);
            _repository = new DeviceRepository(_context);
        }

        [Fact]
        public async Task GetById_ReturnsDevice()
        {
            var id = DeviceFixtures.ActiveDevice.Id;

            var result = await _repository.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetAll_ReturnsAllDevices()
        {
            var result = await _repository.GetAllAsync();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByBrand_ReturnsMatchingDevices()
        {
            var result = await _repository.GetByBrandAsync("Sony");

            Assert.All(result, d => Assert.Equal("Sony", d.Brand));
        }

        [Fact]
        public async Task GetByState_ReturnsMatchingDevices()
        {
            var result = await _repository.GetByStateAsync(DeviceState.InUse);

            Assert.All(result, d => Assert.Equal(DeviceState.InUse, d.State));
        }

        [Fact]
        public async Task AddAsync_PersistsDevice()
        {
            var device = new Device
            {
                Id = Guid.NewGuid(),
                Name = "New",
                Brand = "LG",
                State = DeviceState.Active,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(device);

            var result = await _repository.GetByIdAsync(device.Id);
            Assert.NotNull(result);
            Assert.Equal("New", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ModifiesDevice()
        {
            var device = DeviceFixtures.ActiveDevice;
            device.Name = "Updated";

            await _repository.UpdateAsync(device);

            var result = await _repository.GetByIdAsync(device.Id);
            Assert.Equal("Updated", result!.Name);
        }

        [Fact]
        public async Task DeleteAsync_RemovesDevice()
        {
            var device = DeviceFixtures.InactiveDevice;

            await _repository.DeleteAsync(device);

            var result = await _repository.GetByIdAsync(device.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrue_WhenExists()
        {
            var exists = await _repository.ExistsAsync(DeviceFixtures.ActiveDevice.Id);
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalse_WhenMissing()
        {
            var exists = await _repository.ExistsAsync(Guid.NewGuid());
            Assert.False(exists);
        }
    }
}
