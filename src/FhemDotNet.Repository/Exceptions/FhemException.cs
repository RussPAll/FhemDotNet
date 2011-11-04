using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public abstract class FhemException : Exception
    {
        public FhemException(string message)
            : base(message) { }

        public FhemException(string message, Exception exc)
            : base(message, exc) { }
    }
}
