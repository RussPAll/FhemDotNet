using System;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public abstract class FhemException : Exception
    {
        protected FhemException(string message)
            : base(message) { }

        protected FhemException(string message, Exception exc)
            : base(message, exc) { }
    }
}
