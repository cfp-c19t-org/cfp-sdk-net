using System;

namespace c19t.SDK.CFP.Models.Exceptions
{
    public class CfpPiiValidationException : Exception
    {
        public string ValidationError { get; internal set; }

        public CfpPiiValidationException(string validationError)
        {
            ValidationError = validationError;
        }
    }
}
