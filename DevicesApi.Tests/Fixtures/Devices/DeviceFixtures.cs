using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Fixtures.Devices
{
    public static class DeviceFixtures
    {
        public static readonly Device ActiveDevice = new()
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Name = "Router X",
            Brand = "Sony",
            State = DeviceState.Active
        };

        public static readonly Device InUseDevice = new()
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Name = "Switch Y",
            Brand = "Sony",
            State = DeviceState.InUse
        };

        public static readonly Device InactiveDevice = new()
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            Name = "Switch Z",
            Brand = "Xiomi",
            State = DeviceState.Inactive
        };

        public static readonly List<Device> AllDevices = new()
        {
            ActiveDevice,
            InUseDevice,
            InactiveDevice
        };
    }
}
