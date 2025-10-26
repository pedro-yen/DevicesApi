using DevicesApi.Common.Devices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Common.Devices.DTOs
{
    public class DeviceFilterDto
    {
        public string? Brand {  get; set; }
        public DeviceState? State { get; set; }
    }
}
