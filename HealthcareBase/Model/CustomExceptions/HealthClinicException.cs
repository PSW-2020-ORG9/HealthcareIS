// File:    HealthClinicException.cs
// Author:  Lana
// Created: 02 June 2020 13:49:43
// Purpose: Definition of Class HealthClinicException

using System;

namespace HealthcareBase.Model.CustomExceptions
{
    public class HealthClinicException : Exception
    {
        public HealthClinicException()
        {
        }

        public HealthClinicException(string message) : base(message)
        {
        }
    }
}