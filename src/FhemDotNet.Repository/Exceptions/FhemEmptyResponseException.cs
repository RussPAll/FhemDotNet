using System;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public class FhemEmptyResponseException : FhemException
    {
        public FhemEmptyResponseException(string command)
            : base(string.Format(Resources.ExceptionMessages.FhemEmptyResponseExceptionMessage,
                command)) { }
    }
}
