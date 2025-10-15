using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using DevicesApi.Data.Repositories;
using DevicesApi.Tests.Fixtures.Devices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Fixtures.Repository
{
    public static class DeviceRepositoryFixture
    {
        public static Mock<IDeviceRepository> CreateMock()
        {
            var mock = new Mock<IDeviceRepository>();

            // GetByIdAsync
            mock.Setup(r => r.GetByIdAsync(DeviceFixtures.ActiveDevice.Id))
                .ReturnsAsync(DeviceFixtures.ActiveDevice);

            mock.Setup(r => r.GetByIdAsync(DeviceFixtures.InUseDevice.Id))
                .ReturnsAsync(DeviceFixtures.InUseDevice);

            mock.Setup(r => r.GetByIdAsync(DeviceFixtures.InactiveDevice.Id))
                .ReturnsAsync(DeviceFixtures.InactiveDevice);

            // GetAllAsync
            mock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(DeviceFixtures.AllDevices);

            // GetByBrandAsync
            mock.Setup(r => r.GetByBrandAsync("Sony"))
                .ReturnsAsync(DeviceFixtures.AllDevices.Where(d => d.Brand == "Sony").ToList());

            // GetByStateAsync
            mock.Setup(r => r.GetByStateAsync(DeviceState.InUse))
                .ReturnsAsync(DeviceFixtures.AllDevices.Where(d => d.State == DeviceState.InUse).ToList());

            // AddAsync
            mock.Setup(r => r.AddAsync(It.IsAny<Device>()))
                .Returns(Task.CompletedTask);

            // UpdateAsync
            mock.Setup(r => r.UpdateAsync(It.IsAny<Device>()))
                .Returns(Task.CompletedTask);

            // DeleteAsync
            mock.Setup(r => r.DeleteAsync(It.IsAny<Device>()))
                .Returns(Task.CompletedTask);

            return mock;
        }
    }
}
