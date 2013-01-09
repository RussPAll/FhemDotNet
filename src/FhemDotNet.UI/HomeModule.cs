using System.Linq;
using System.Web.Mvc;
using FhemDotNet.Repository.Interfaces;
using Nancy;
using FhemDotNet.UI.Models;

namespace FhemDotNet.UI
{
    [HandleError]
    public class HomeModule : NancyModule
    {
        public HomeModule(IThermostatRepository thermostatRepository)
        {
            Get["/"] = parameters =>
                {
                    var model = thermostatRepository.GetThermostatList();
                    var viewModel = model.Select(x => new ThermostatViewModel
                        {
                            Name = x.Name
                        }).ToList();
                    return View["Index", viewModel];
                };
        }
    }
}
