// File:    AuthenticationException.cs
// Author:  Lana
// Created: 02 June 2020 14:00:57
// Purpose: Definition of Class AuthenticationException

using System;

namespace Model.CustomExceptions
{
    public class AuthenticationException : ValidationException
    {
        public AuthenticationException() : base()
        {

        }

        public AuthenticationException(String message) : base(message)
        {

        }
    }
}