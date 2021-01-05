using System;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public TimeInterval(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateTime Start { get; internal set; }
        public DateTime End { get; internal set; }
    }
}
