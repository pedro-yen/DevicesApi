using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Common.Exceptions;
using DevicesApi.Data.Entities;
using DevicesApi.Data.Repositories;
using DevicesApi.Tests.Fixtures.Devices;
using DevicesApi.Tests.Fixtures.Repository;

using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Test.Manager.BusinessManager
{
    public class DeviceBusinessManagerTests
    {
        private readonly DeviceBusinessManager _manager;

        public DeviceBusinessManagerTests()
        {
            var mockRepo = DeviceRepositoryFixture.CreateMock();
            _manager = new DeviceBusinessManager(mockRepo.Object);
        }

        [Fact]
        public async Task GetById_ReturnsDevice()
        {
            var id = DeviceFixtures.ActiveDevice.Id;

            var result = await _manager.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(DeviceFixtures.ActiveDevice.Name, result.Name);
        }

        [Fact]
        public async Task GetById_ThrowsNotFound_WhenDeviceMissing()
        {
            var id = Guid.NewGuid();

            await Assert.ThrowsAsync<NotFoundException>(() => _manager.GetByIdAsync(id));
        }

        [Fact]
        public async Task PartialUpdateDto_ThrowsValidation_WhenInUseNameOrBrandChanged()
        {
            var id = DeviceFixtures.InUseDevice.Id;
            var dto = new DeviceUpdateDto
            {
                Name = "Changed",
                Brand = "ChangedBrand",
                State = DeviceState.InUse
            };

            await Assert.ThrowsAsync<ValidationException>(() => _manager.PartialUpdateAsync(id, dto));
        }

        [Fact]
        public async Task Delete_ThrowsValidation_WhenDeviceInUse()
        {
            var id = DeviceFixtures.InUseDevice.Id;

            await Assert.ThrowsAsync<ValidationException>(() => _manager.DeleteAsync(id));
        }

        [Fact]
        public async Task Create_SetsIdAndCreatedAt()
        {
            var device = new Device { Name = "New", Brand = "Sony", State = DeviceState.Active };

            var result = await _manager.CreateAsync(device);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 5);

        }
    }
}
