using System;
using System.Linq;
using FhemDotNet.Domain;
using FhemDotNet.Host.Mappers;
using Nancy;

namespace FhemDotNet.Host
{
    public class DevicesModule : NancyModule
    {
        public DevicesModule(IDevicesInteractor devicesInteractor)
        {
            Get["/devices"] = GetDeviceList(devicesInteractor);
            Put["/devices/{deviceName}/desiredTempCommand"] = SetDesiredTemp(devicesInteractor);
        }

        private static Func<dynamic, dynamic> GetDeviceList(IDevicesInteractor devicesInteractor)
        {
            return arg =>
                {
                    var model = devicesInteractor.GetDeviceList();
                    return model.Select(ThermostatMapper.DomainToViewModel).ToList();
                };
        }

        private Func<dynamic, dynamic> SetDesiredTemp(IDevicesInteractor thermostatRepository)
        {
            return parameters => Response;
        }
    }
}
