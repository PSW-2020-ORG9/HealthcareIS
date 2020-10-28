using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CustomExceptions
{
    class TimingException : ValidationException
    {
        public TimingException() : base()
        {

        }

        public TimingException(String message) : base(message)
        {

        }
    }
}
