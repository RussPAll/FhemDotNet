namespace FhemDotNet.Host.Models
{
    public class ThermostatViewModel
    {
        public string Name { get; set; }
        public string CurrentTemp { get; set; }
        public float DesiredTemp { get; set; }
        public string PendingDesiredTemp { get; set; }
        public string Mode { get; set; }
        public string Actuator { get; set; }

        public DayScheduleViewModel[] DaySchedules { get; set; }
    }
}