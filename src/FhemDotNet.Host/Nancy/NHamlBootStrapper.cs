using System;
using System.Configuration;
using System.Net.NetworkInformation;
using FhemDotNet.Domain;
using FhemDotNet.Repository;
using FhemDotNet.Repository.Interfaces;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.ViewEngines.NHaml;

namespace FhemDotNet.Host.Nancy
{
    public class NHamlBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<NHamlViewEngine>();
            RegisterCorrectThermostatRepository(container);
        }

        private void RegisterCorrectThermostatRepository(TinyIoCContainer container)
        {
            var serverName = ConfigurationManager.AppSettings["FhemServerName"];
            if (IsServerAccessible(serverName))
            {
                container.Register<IThermostatRepository, ThermostatRepository>().AsSingleton();
                container.Register<ITelnetConnection>(
                    (i, n) => new TelnetConnection(serverName,
                                                   Int32.Parse(ConfigurationManager.AppSettings["FhemServerPort"])));
            }
            else
                container.Register<IThermostatRepository, FakeThermostatRepository>().AsSingleton();
        }

        private bool IsServerAccessible(string serverName)
        {
            PingReply pingReply = null;
            try
            {
                var buffer = new byte[32];
                pingReply = new Ping().Send(
                    serverName, 1000, buffer, new PingOptions(32, true));
            }
            catch (PingException) { }
            return pingReply != null && pingReply.Status == IPStatus.Success;

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