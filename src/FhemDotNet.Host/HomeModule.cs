using Nancy;

namespace FhemDotNet.Host
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Index"];
        }
    }
}
