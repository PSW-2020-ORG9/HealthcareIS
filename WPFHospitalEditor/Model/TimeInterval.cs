using System;

namespace WPFHospitalEditor.DTOs
{
    public class TimeInterval
    {
        public TimeInterval() { }
        public TimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration => End - Start;
    }
}