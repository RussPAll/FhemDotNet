using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FhemDotNet.Domain;
using FhemDotNet.Repository.Interfaces;

namespace FhemDotNet.Repository
{
    public class FakeThermostatRepository : IThermostatRepository
    {
        private static readonly IList<Thermostat> Thermostats;

        static FakeThermostatRepository()
        {
            Thermostats = new List<Thermostat>
                               {
                                   new Thermostat
                                       {
                                           CurrentTemp = 19,
                                           DesiredTemp = 20,
                                           Mode = ThermostatMode.Auto,
                                           Name = "Bedroom"
                                       },
                                       
                                   new Thermostat
                                       {
                                           CurrentTemp = 18,
                                           DesiredTemp = 24,
                                           Mode = ThermostatMode.Manu,
                                           Name = "LivingRoom"
                                       }
                               };
        }

        public IList<Thermostat> GetThermostatList()
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
