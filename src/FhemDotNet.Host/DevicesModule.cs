﻿using System.Collections.Generic;
using System.Linq;
using FhemDotNet.Host.Mappers;
using FhemDotNet.Host.Models;
using FhemDotNet.Repository.Interfaces;
using Nancy;

namespace FhemDotNet.Host
{
    public class DevicesModule : NancyModule
    {
        public DevicesModule(IThermostatRepository thermostatRepository)
        {
            Get["/devices"] = _ =>
                                  {
                                      var model = thermostatRepository.GetThermostatList();
                                      return model.Select(x => ThermostatMapper.DomainToViewModel(x)).ToList();
                                  };

            Get["/devices/{deviceName}"] = parameters =>
            {
                var model = thermostatRepository.GetThermostatByName(parameters.deviceName);
                var viewModel = ThermostatMapper.DomainToViewModel(model);
                return View["Thermostat", viewModel];
            };
        }
    }
}