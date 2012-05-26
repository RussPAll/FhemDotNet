using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FhemDotNet.UI.Models;

namespace FhemDotNet.UI.Mappers
{
    public class ThermostatMapper
    {
        public static ThermostatViewModel DomainToViewModel(Domain.Thermostat input)
        {
            return new ThermostatViewModel
            {
                Name = input.Name,
                CurrentTemp = input.CurrentTemp.ToString(),
                DesiredTemp = input.DesiredTemp.ToString(),
                Mode = input.Mode == Domain.ThermostatMode.Auto ? "Auto" : "Manu"
            };
        }
    }
}