// File:    ValidationException.cs
// Author:  Lana
// Created: 02 June 2020 13:52:02
// Purpose: Definition of Class ValidationException

namespace Model.CustomExceptions
{
    public class ValidationException : HealthClinicException
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }
    }
}