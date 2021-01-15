using System;
using System.Windows;

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

        public bool IsValid()
        {
            if (Start < End) return true;
            MessageBox.Show("End time must be after start time!", "");
            return false;
        }
    }
}
