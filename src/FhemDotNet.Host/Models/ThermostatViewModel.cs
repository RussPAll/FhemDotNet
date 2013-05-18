using FhemDotNet.Domain;

namespace FhemDotNet.Host.Models
{
    public class ThermostatViewModel
    {
        public string Name { get; set; }
        public MeasurementViewModel<string> CurrentTemp { get; set; }
        public MeasurementViewModel<float> DesiredTemp { get; set; }
        public MeasurementViewModel<string> Mode { get; set; }
        public MeasurementViewModel<string> Actuator { get; set; }

        public DayScheduleViewModel[] DaySchedules { get; set; }
    }
}