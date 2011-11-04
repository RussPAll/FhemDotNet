using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FhemDotNet.Domain
{
    public enum ThermostatMode
    {
        [Description("Manual")]
        Manu,
        [Description("Auto")]
        Auto
    }
}
