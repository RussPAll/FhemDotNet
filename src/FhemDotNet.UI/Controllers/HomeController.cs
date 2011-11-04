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

namespace FhemDotNet.UI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        readonly IThermostatRepository _thermostatRepository;

        public HomeController()
            : base()
        {
            TelnetConnection telnetConnection = new TelnetConnection(ConfigHelper.FhemServerName,
                ConfigHelper.FhemServerPort);
            _thermostatRepository = new ThermostatRepository(telnetConnection, 1000);
        }

        public HomeController(IThermostatRepository thermostatRepository)
            : base()
        {
            #region Parameter validation
            thermostatRepository.RequireArgument("thermostatRepository").IsNotNull();
            #endregion

            _thermostatRepository = thermostatRepository;
        }
        
        public ActionResult Index()
        {
            ViewData.Model = _thermostatRepository.GetThermostatList();
            return View();
        }

    }
}
