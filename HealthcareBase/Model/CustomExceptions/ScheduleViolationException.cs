// File:    ScheduleViolationException.cs
// Author:  Lana
// Created: 02 June 2020 14:00:57
// Purpose: Definition of Class ScheduleViolationException

using System;

namespace Model.CustomExceptions
{
    public class ScheduleViolationException : ValidationException
    {
        public ScheduleViolationException() : base()
        {

        }

        public ScheduleViolationException(String message) : base(message)
        {

        }
    }
}