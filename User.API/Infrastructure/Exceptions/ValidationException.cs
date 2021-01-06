// File:    ValidationException.cs
// Author:  Lana
// Created: 02 June 2020 13:52:02
// Purpose: Definition of Class ValidationException


using System;

namespace User.API.Infrastructure.Exceptions
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