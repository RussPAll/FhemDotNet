namespace FhemDotNet.Domain
{
    public class Thermostat
    {
        public string Name { get; set; }
        public Measurement<float?> CurrentTemp { get; set; }
        public Measurement<float?> DesiredTemp { get; set; }
        public Measurement<ThermostatMode> Mode { get; set; }
        public Measurement<string> Actuator { get; set; }
        public DaySchedule[] Schedule { get; set; }

        public Thermostat()
        {
            CurrentTemp = null;
            Schedule = new DaySchedule[7];
            for (int c = 0; c < 7; c++)
                Schedule[c] = new DaySchedule();
        }

        public override string ToString()
        {
            return string.Format("Thermostat {0}, CurrentTemp = {1}", Name, CurrentTemp);
        }
    }
}
