using System;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public DateTime Start { get; internal set; }
        public DateTime End { get; internal set; }

        public TimeInterval(DateTime startDate, DateTime endDate)
        {
            Start = startDate;
            End = endDate;
        }
    }
}
