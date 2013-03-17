using System;

namespace FhemDotNet.Domain
{
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