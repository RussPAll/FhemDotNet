using System;
using System.Collections.Generic;
using System.Linq;
using FhemDotNet.Domain;

namespace FhemDotNet.Repository
{
    public class FakeThermostatRepository : IThermostatRepository
    {
        private static readonly IList<Thermostat> Thermostats;

        static FakeThermostatRepository()
        {
            Thermostats = new ThermostatList
                {
                    new Thermostat
                        {
                            CurrentTemp = new Measurement<float?>(19, DateTime.Now),
                            DesiredTemp = new Measurement<float?>(20, DateTime.Now),
                            Mode = new Measurement<ThermostatMode>(ThermostatMode.Auto, DateTime.Now),
                            Name = "Bedroom"
                        },

                    new Thermostat
                        {
                            CurrentTemp = new Measurement<float?>(18, DateTime.Now),
                            DesiredTemp = new Measurement<float?>(24, DateTime.Now),
                            Mode = new Measurement<ThermostatMode>(ThermostatMode.Manu, DateTime.Now),
                            Name = "LivingRoom"
                        }
                };
        }

        public List<Thermostat> GetThermostatList()
        {
            return (List<Thermostat>)Thermostats;
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
