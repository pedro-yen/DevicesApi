using DevicesApi.Data;
using DevicesApi.Tests.Fixtures.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Fixtures.Data
{
    public static class DbSeeder
    {
        public static void SeedDevices(AppDbContext context)
        {
            context.Devices.AddRange(DeviceFixtures.AllDevices);
            context.SaveChanges();
        }
    }
}
