using System;
using System.Windows;

namespace WPFHospitalEditor.Model
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeInterval() { }
        public TimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool IsValid()
        {
            if (Start < End) return true;
            MessageBox.Show("End time must be after start time!", "");
            return false;
        }
    }
}
