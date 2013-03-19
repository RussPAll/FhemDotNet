using System;
using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
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
            var serverName = ConfigurationManager.AppSettings["FhemServerName"];

            base.ConfigureRequestContainer(container, context);

            if (IsServerAccessible(serverName))
                container.Register<IThermostatRepository, ThermostatRepository>();
            else
                container.Register<IThermostatRepository, FakeThermostatRepository>();
            container.Register<ITelnetConnection>(
                (i, n) => new TelnetConnection(serverName,
                                               Int32.Parse(ConfigurationManager.AppSettings["FhemServerPort"])));
            container.Register<NHamlViewEngine>();
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
            catch (PingException e) {}
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