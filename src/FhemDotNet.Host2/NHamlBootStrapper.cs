

namespace FhemDotNet.Host
{
    using Nancy;
    using global::Nancy.ViewEngines.NHaml;
    
    public class NHamlBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoC.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<NHamlViewEngine>();
        }
    }
}