using System;

namespace FhemDotNet.Host.Models
{
    public class MeasurementViewModel<T>
    {
        public MeasurementViewModel(T value, DateTime timestamp)
        {
            Timestamp = timestamp.ToShortDateString() + " " + timestamp.ToShortTimeString();
            Value = value;
        }

        public T Value { get; private set; }
        public string Timestamp { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Value, Timestamp);
        }
    }
}