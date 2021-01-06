using System;

namespace Schedule.API.Model.Exceptions
{
    public class ScheduleViolationException : Exception
    {
        public ScheduleViolationException(string message) : base(message)
        {
        }
    }
}