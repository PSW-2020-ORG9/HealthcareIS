using System;

namespace Feedback.API.Infrastructure.Exceptions.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }
    }
}