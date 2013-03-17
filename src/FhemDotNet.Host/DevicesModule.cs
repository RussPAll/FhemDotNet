using System;
using System.Linq;
using FhemDotNet.Domain;
using FhemDotNet.Host.Mappers;
using Nancy;

namespace FhemDotNet.Host
{
    public class DevicesModule : NancyModule
    {
        public DevicesModule(DevicesInteractor devicesInteractor)
        {
            Get["/devices"] = GetDeviceList(devicesInteractor);
            Put["/devices/{deviceName}/desiredTempCommand"] = SetDesiredTemp(devicesInteractor);
        }

        private static Func<dynamic, dynamic> GetDeviceList(DevicesInteractor devicesInteractor)
        {
            return arg =>
                {
                    var model = devicesInteractor.GetDeviceList();
                    return model.Select(ThermostatMapper.DomainToViewModel).ToList();
                };
        }

        private Func<dynamic, dynamic> SetDesiredTemp(DevicesInteractor thermostatRepository)
        {
            return parameters => Response;
        }
    }
}
