using FhemDotNet.Host.Models;

namespace FhemDotNet.Host.Mappers
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