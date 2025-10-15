using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Common.Devices.Enums
{
    /// <summary>
    /// Represents the operational state of a device.
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// The device is available and ready for use.
        /// </summary>
        [Description("Active")]
        Active,

        /// <summary>
        /// The device is currently in use or assigned.
        /// </summary>
        [Description("InUse")]
        InUse,

        /// <summary>
        /// The device is not active and may be decommissioned or offline.
        /// </summary>
        [Description("Inactive")]
        Inactive
    }
}
