// File:    BadRequestException.cs
// Author:  Lana
// Created: 02 June 2020 13:53:33
// Purpose: Definition of Class BadRequestException

namespace HealthcareBase.Model.CustomExceptions
{
    public class BadRequestException : HealthClinicException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}