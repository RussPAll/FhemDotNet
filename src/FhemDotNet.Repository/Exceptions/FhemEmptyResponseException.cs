using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FhemDotNet.Repository.Exceptions
{
    [Serializable]
    public class FhemEmptyResponseException : FhemException
    {
        public FhemEmptyResponseException(string command)
            : base(string.Format(CultureInfo.InvariantCulture,
                Resources.ExceptionMessages.FhemEmptyResponseExceptionMessage,
                command)) { }

        public FhemEmptyResponseException(string command, Exception exc)
            : base(string.Format(CultureInfo.InvariantCulture,
                Resources.ExceptionMessages.FhemEmptyResponseExceptionMessage,
                command), exc) { }
    }
}
