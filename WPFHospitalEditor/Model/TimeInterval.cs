using System;
using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public TimeInterval()
        {

        }
        public TimeInterval(DateTime start, DateTime end)
        {
            Validate(start, end);
            Start = start;
            End = end;
        }

        public void Validate(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new ValidationException("End time must be after start time!");
        }
    }
}
