using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FhemDotNet.UI.Models
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