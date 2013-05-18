using System.Collections.Generic;

namespace FhemDotNet.Domain
{
    public interface IDevicesInteractor
    {
        IEnumerable<Thermostat> GetDeviceList();
    }

    public class DevicesInteractor : IDevicesInteractor
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
