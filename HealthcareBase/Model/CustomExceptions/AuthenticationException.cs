// File:    AuthenticationException.cs
// Author:  Lana
// Created: 02 June 2020 14:00:57
// Purpose: Definition of Class AuthenticationException

namespace Model.CustomExceptions
{
    public class AuthenticationException : ValidationException
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string message) : base(message)
        {
        }
    }
}