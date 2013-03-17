using System.Collections.Generic;

namespace FhemDotNet.Domain
{
    public class DevicesInteractor
    {
        private readonly IThermostatRepository _repoository;

        public DevicesInteractor(IThermostatRepository repoository)
        {
            _repoository = repoository;
        }

        public IEnumerable<Thermostat> GetDeviceList()
        {
            return ThermostatList.Load(_repoository);
        }
    }
}
