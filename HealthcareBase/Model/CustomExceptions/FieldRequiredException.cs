using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CustomExceptions
{
    class FieldRequiredException : ValidationException
    {
        public FieldRequiredException() : base()
        {

        }

        public FieldRequiredException(String message) : base(message)
        {

        }
    }
}
