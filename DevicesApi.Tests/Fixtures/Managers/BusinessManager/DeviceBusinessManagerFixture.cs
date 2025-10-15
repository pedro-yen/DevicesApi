using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using DevicesApi.Tests.Fixtures.Devices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Fixtures.Managers.BusinessManager
{
    public static class DeviceBusinessManagerFixture
    {
        public static Mock<IDeviceBusinessManager> CreateMock()
        {
            var mock = new Mock<IDeviceBusinessManager>();

            mock.Setup(m => m.GetByIdAsync(DeviceFixtures.ActiveDevice.Id))
                .ReturnsAsync(DeviceFixtures.ActiveDevice);

            mock.Setup(m => m.GetAllAsync())
                .ReturnsAsync(DeviceFixtures.AllDevices);

            mock.Setup(m => m.GetByBrandAsync("Sony"))
                .ReturnsAsync(DeviceFixtures.AllDevices.Where(d => d.Brand == "Sony").ToList());

            mock.Setup(m => m.GetByStateAsync(DeviceState.InUse))
                .ReturnsAsync(DeviceFixtures.AllDevices.Where(d => d.State == DeviceState.InUse).ToList());

            mock.Setup(m => m.CreateAsync(It.IsAny<Device>()))
                .ReturnsAsync((Device d) =>
                {
                    d.Id = Guid.NewGuid();
                    return d;
                });

            mock.Setup(m => m.PartialUpdateAsync(DeviceFixtures.InUseDevice.Id, It.IsAny<DeviceUpdateDto>()))
                .ReturnsAsync((Guid id, DeviceUpdateDto dto) => new Device
                {
                    Id = id,
                    Name = dto.Name!,
                    Brand = dto.Brand!,
                    State = dto.State ?? DeviceState.InUse
                });

            mock.Setup(m => m.DeleteAsync(DeviceFixtures.InactiveDevice.Id))
                .Returns(Task.CompletedTask);

            return mock;
        }
    }
}
