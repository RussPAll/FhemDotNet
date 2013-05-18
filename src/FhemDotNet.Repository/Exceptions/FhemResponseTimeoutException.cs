using System;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public class FhemResponseTimeoutException : FhemException
    {
        public FhemResponseTimeoutException(string command, int timeoutMilliseconds, Exception exc)
            : base(string.Format(Resources.ExceptionMessages.FhemResponseTimeoutExceptionMessage,
                command, timeoutMilliseconds), exc) { }
    }
}
