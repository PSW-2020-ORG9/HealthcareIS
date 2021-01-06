// File:    BadRequestException.cs
// Author:  Lana
// Created: 02 June 2020 13:53:33
// Purpose: Definition of Class BadRequestException

using System;

namespace User.API.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}