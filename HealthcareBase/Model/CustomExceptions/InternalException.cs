// File:    InternalException.cs
// Author:  Lana
// Created: 02 June 2020 13:56:46
// Purpose: Definition of Class InternalException

namespace Model.CustomExceptions
{
    public class InternalException : HealthClinicException
    {
        public InternalException()
        {
        }

        public InternalException(string message) : base(message)
        {
        }
    }
}