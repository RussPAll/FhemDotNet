using System;
using System.Linq;
using FhemDotNet.Domain;

namespace FhemDotNet.Repository
{
    public class FakeThermostatRepository : IThermostatRepository
    {
        private static readonly ThermostatList Thermostats;

        static FakeThermostatRepository()
        {
            Thermostats = new ThermostatList
                {
                    new Thermostat
                        {
                            Actuator = new Measurement<string>("55%", DateTime.Now),
                            CurrentTemp = new Measurement<float?>(19, DateTime.Now),
                            DesiredTemp = new Measurement<float?>(20, DateTime.Now),
                            Mode = new Measurement<ThermostatMode>(ThermostatMode.Auto, DateTime.Now),
                            Name = "Bedroom"
                        },

                    new Thermostat
                        {
                            Actuator = new Measurement<string>("55%", DateTime.Now),
                            CurrentTemp = new Measurement<float?>(18, DateTime.Now),
                            DesiredTemp = new Measurement<float?>(24, DateTime.Now),
                            Mode = new Measurement<ThermostatMode>(ThermostatMode.Manu, DateTime.Now),
                            Name = "LivingRoom"
                        }
                };
        }

        public ThermostatList GetThermostatList()
        {
            return Thermostats;
        }

        public Thermostat GetThermostatByName(string deviceName)
        {
            return Thermostats.FirstOrDefault(x => x.Name == deviceName);
        }

        public void SetThermostatDesiredTemp(float newDesiredTemp)
        {
            throw new NotImplementedException();
        }
    }
}
