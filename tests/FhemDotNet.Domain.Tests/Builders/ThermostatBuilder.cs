using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FhemDotNet.Domain.Tests.Builders
{
    public static class ThermostatBuilder
    {
        public static Thermostat Build()
        {
            return Build("TestThermostat", 19, 20);
        }

        public static Thermostat Build(string thermostatName, float currentTemp, float desiredTemp)
        {
            return new Thermostat
            {
                Name = thermostatName,
                CurrentTemp = currentTemp,
                DesiredTemp = desiredTemp,
                Mode = ThermostatMode.Manu
            };
        }
    }
}
