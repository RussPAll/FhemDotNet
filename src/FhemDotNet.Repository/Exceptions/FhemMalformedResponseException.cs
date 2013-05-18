using System;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public class FhemMalformedResponseException : FhemException
    {
        public FhemMalformedResponseException(string errorDetail)
            : base(string.Format(Resources.ExceptionMessages.FhemMalformedResponseExceptionMessage,
                errorDetail)) { }
    }
}
