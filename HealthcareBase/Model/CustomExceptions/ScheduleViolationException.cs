// File:    ScheduleViolationException.cs
// Author:  Lana
// Created: 02 June 2020 14:00:57
// Purpose: Definition of Class ScheduleViolationException

namespace HealthcareBase.Model.CustomExceptions
{
    public class ScheduleViolationException : ValidationException
    {
        public ScheduleViolationException()
        {
        }

        public ScheduleViolationException(string message) : base(message)
        {
        }
    }
}