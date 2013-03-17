using FhemDotNet.Domain;

namespace FhemDotNet.Host.Models
{
    public class ThermostatViewModel
    {
        public string Name { get; set; }
        public Measurement<string> CurrentTemp { get; set; }
        public Measurement<float> DesiredTemp { get; set; }
        public Measurement<string> Mode { get; set; }
        public Measurement<string> Actuator { get; set; }

        public DayScheduleViewModel[] DaySchedules { get; set; }
    }
}