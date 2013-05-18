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
                Mode = new Measurement<ThermostatMode>(ThermostatMode.Manu, DateTime.Now),
                Actuator = new Measurement<string>("75", DateTime.Now)
            };
        }

        public static Thermostat WithTimePeriod(this Thermostat thermostat, DayOfWeek dayOfWeek, TimePeriod period)
        {
            thermostat.Schedule[(int)DayOfWeek.Sunday].AddPeriod(period);
            return thermostat;
        }
    }
}
