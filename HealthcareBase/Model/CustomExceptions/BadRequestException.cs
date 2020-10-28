// File:    BadRequestException.cs
// Author:  Lana
// Created: 02 June 2020 13:53:33
// Purpose: Definition of Class BadRequestException

using System;

namespace Model.CustomExceptions
{
    public class BadRequestException : HealthClinicException
    {
        public BadRequestException() : base()
        {

        }

        public BadRequestException(String message) : base(message)
        {

        }
    }
}