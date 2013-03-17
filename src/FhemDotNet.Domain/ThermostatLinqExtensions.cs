using System.Collections.Generic;

namespace FhemDotNet.Domain
{
    public static class ThermostatLinqExtensions
    {
        public static ThermostatList ToThermostatList(this IEnumerable<Thermostat> seq)
        {
            var outList = new ThermostatList();
            outList.AddRange(seq);
            return outList;
        }
    }
}