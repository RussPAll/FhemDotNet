using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FhemDotNet.Domain
{
    public class Thermostat
    {
        public string Name { get; set; }
        public float? CurrentTemp { get; set; }
        public float? DesiredTemp { get; set; }
        public ThermostatMode Mode { get; set; }

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
            return string.Format(CultureInfo.InvariantCulture, Resources.ToString.Thermostat, Name, CurrentTemp);
        }
    }

    public class DaySchedule
    {
        private readonly IList<TimePeriod> _periods = new List<TimePeriod>();

        public IEnumerable<TimePeriod> Periods
        {
            get { return _periods.ToList(); }
        }

        public void AddPeriod(TimePeriod timePeriod)
        {
            _periods.Add(timePeriod);
        }
    }

    public class TimePeriod
    {
        public TimePeriod(DateTime fromTime, DateTime toTime)
        {
            FromTime = fromTime;
            ToTime = toTime;
        }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
