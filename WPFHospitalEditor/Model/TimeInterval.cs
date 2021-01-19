using System;
using System.Windows;
using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeInterval() { }
        public TimeInterval(DateTime start, DateTime end)
        {
            if(Validate(start, end))
            {
                Start = start;
                End = end;
            }
        }

        public bool Validate(DateTime start, DateTime end)
        {
            if (start < end) return true;
            throw new ValidationException("End time must be after start time!");
        }
    }
}
