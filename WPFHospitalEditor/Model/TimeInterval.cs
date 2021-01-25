using System;
using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeInterval() { }

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
