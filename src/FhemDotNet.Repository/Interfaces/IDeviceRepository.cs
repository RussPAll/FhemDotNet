using FhemDotNet.Domain;
using System.Collections.Generic;

namespace FhemDotNet.Repository.Interfaces
{
    public interface IThermostatRepository
    {
        IList<Thermostat> GetThermostatList();
    }
}
