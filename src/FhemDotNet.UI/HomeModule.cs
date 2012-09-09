using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FhemDotNet.Domain;
using FhemDotNet.Repository;
using FhemDotNet.Repository.Interfaces;
using FhemDotNet.CrossCutting.Validation;
using FhemDotNet.CrossCutting;
using Nancy;
using FhemDotNet.UI.Models;

namespace FhemDotNet.UI
{
    [HandleError]
    public class HomeModule : NancyModule
    {
        public HomeModule()
            : this(new ThermostatRepository(new TelnetConnection("localhost", 7072), 1000))
        { }

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

            Post["/"] = parameters =>
                {
                    var model = thermostatRepository.GetThermostatList();
                    return View["Index", model];
                };
        }
    }
}
