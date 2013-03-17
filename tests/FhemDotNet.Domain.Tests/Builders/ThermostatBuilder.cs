using System;

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
                CurrentTemp = new Measurement<float?>(currentTemp, DateTime.Now),
                DesiredTemp = new Measurement<float?>(desiredTemp, DateTime.Now),
                Mode = new Measurement<ThermostatMode>(ThermostatMode.Manu, DateTime.Now)
            };
        }
    }
}
