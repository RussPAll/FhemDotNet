using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FhemDotNet.Domain
{
    public class Thermostat
    {
        public string Name { get; set; }
        public float? CurrentTemp { get; set; }
        public float? DesiredTemp { get; set; }
        public ThermostatMode Mode { get; set; }

        public Thermostat()
        {
            CurrentTemp = null;
        }

        public override string ToString()
        {   
            return string.Format(CultureInfo.InvariantCulture, Resources.ToString.Thermostat, Name, CurrentTemp);
        }
    }
}
