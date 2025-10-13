using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevicesApi.

using DevicesApi.Common.Devices.Enums;
namespace DevicesApi.Common.Devices.DTOs
{
    public class DeviceCreateDto
    {
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public DeviceState State { get; set; }
    }
}
