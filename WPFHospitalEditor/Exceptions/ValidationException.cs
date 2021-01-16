using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Exceptions
{
    class ValidationException : Exception
    {

        public ValidationException() { }
        public ValidationException(string message) : base(message) { }

    }
}
