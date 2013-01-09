using System;
using System.Configuration;
using FhemDotNet.Repository;
using FhemDotNet.Repository.Interfaces;
using Nancy;
using Nancy.ViewEngines.NHaml;

namespace FhemDotNet.Host
{
    public class NHamlBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoC.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<IThermostatRepository, FakeThermostatRepository>();
            container.Register<ITelnetConnection>(
                (i, n) => new TelnetConnection(ConfigurationManager.AppSettings["FhemServerName"],
                                               Int32.Parse(ConfigurationManager.AppSettings["FhemServerPort"])));
            container.Register<NHamlViewEngine>();
            //container.AutoRegister();
        }
    }
}