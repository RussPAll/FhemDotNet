//using Nancy.ViewEngines.NHaml;

namespace FhemDotNet.Host.Models
{
    public class ThermostatViewModel
    {
        public string Name { get; set; }
        public string CurrentTemp { get; set; }
        public string DesiredTemp { get; set; }
        public string PendingDesiredTemp { get; set; }
        public string Mode { get; set; }
    }
}