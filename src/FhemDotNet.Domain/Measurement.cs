using System;

namespace FhemDotNet.Domain
{
    public class Measurement<T>
    {
        public Measurement(T value, DateTime timestamp)
        {
            Timestamp = timestamp;
            Value = value;
        }

        public T Value { get; private set; }
        public DateTime Timestamp { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ({1:yyyy-MM-dd hh:mm:ss})", Value, Timestamp);
        }
    }
}