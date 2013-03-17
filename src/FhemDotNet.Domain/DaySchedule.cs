using System.Collections.Generic;
using System.Linq;

namespace FhemDotNet.Domain
{
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
}