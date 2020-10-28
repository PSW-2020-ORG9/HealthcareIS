using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CustomExceptions
{
    class BadReferenceException : ValidationException
    {
        public BadReferenceException() : base()
        {

        }

        public BadReferenceException(String message) : base(message)
        {

        }
    }
}
