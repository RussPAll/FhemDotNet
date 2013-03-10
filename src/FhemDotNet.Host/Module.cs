using System;
using System.Linq;
using FhemDotNet.Host.Mappers;
using FhemDotNet.Host.Models;
using FhemDotNet.Repository.Interfaces;
using Nancy;
using Nancy.ModelBinding;

namespace FhemDotNet.Host
{
    public class DevicesModule : NancyModule
    {
        public DevicesModule(IThermostatRepository thermostatRepository)
        {
            Get["/devices"] = GetDeviceList(thermostatRepository);
            Put["/devices/{deviceName}/desiredTempCommand"] = SetDesiredTemp(thermostatRepository);

            //Get["/devices/{deviceName}"] = parameters =>
            //{
            //    var model = thermostatRepository.GetThermostatByName(parameters.deviceName);
            //    var viewModel = ThermostatMapper.DomainToViewModel(model);
            //    return View["Thermostat", viewModel];
            //};
        }

        private static Func<dynamic, dynamic> GetDeviceList(IThermostatRepository thermostatRepository)
        {
            return arg =>
                {
                    var model = thermostatRepository.GetThermostatList();
                    return model.Select(ThermostatMapper.DomainToViewModel).ToList();
                };
        }

        private Func<dynamic, dynamic> SetDesiredTemp(IThermostatRepository thermostatRepository)
        {
            return parameters =>
                {
                    DesiredTempCommand command = this.Bind();
                    thermostatRepository.SetThermostatDesiredTemp(command.NewDesiredTemp);
                    return Response;
                };
        }
    }
}
