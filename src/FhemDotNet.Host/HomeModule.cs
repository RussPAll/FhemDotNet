using System.Linq;
using FhemDotNet.Host.Mappers;
using FhemDotNet.Host.Models;
using FhemDotNet.Repository.Interfaces;
using Nancy;

namespace FhemDotNet.Host
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IThermostatRepository thermostatRepository)
        {
            Get["/"] = parameters =>
                {
                    var model = thermostatRepository.GetThermostatList();
                    var viewModel = model.Select(x => ThermostatMapper.DomainToViewModel(x));
                    return View["Index", viewModel.ToList()];
                };
        }
    }
}
