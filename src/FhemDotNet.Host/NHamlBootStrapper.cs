using System;
using System.Configuration;
using FhemDotNet.Domain;
using FhemDotNet.Repository;
using FhemDotNet.Repository.Interfaces;
using Nancy;
using Nancy.Conventions;
using Nancy.ViewEngines.NHaml;

namespace FhemDotNet.Host
{
    public class NHamlBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<IThermostatRepository, ThermostatRepository>();
            //container.Register<IThermostatRepository, FakeThermostatRepository>();
            container.Register<ITelnetConnection>(
                (i, n) => new TelnetConnection(ConfigurationManager.AppSettings["FhemServerName"],
                                               Int32.Parse(ConfigurationManager.AppSettings["FhemServerPort"])));
            container.Register<NHamlViewEngine>();
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("content", "content"));
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new FhemDotNetRootPathProvider();
            }
        }
    }
}