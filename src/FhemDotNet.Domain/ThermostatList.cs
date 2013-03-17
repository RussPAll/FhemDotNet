using System.Collections.Generic;

namespace FhemDotNet.Domain
{
    public class ThermostatList : List<Thermostat>
    {
        public static ThermostatList Load(IThermostatRepository repository)
        {
            return repository.GetThermostatList();
        }
    }
}
