using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CustomExceptions
{
    class NotUniqueException: ValidationException
    {
        public NotUniqueException() : base()
        {

        }

        public NotUniqueException(String message) : base(message)
        {

        }
    }
}
