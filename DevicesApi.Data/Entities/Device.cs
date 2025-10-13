using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Data.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public DeviceState State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
