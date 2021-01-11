using System;
using System.Windows;

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

        public bool IsValid()
        {
            if (Start >= End)
            {
                MessageBox.Show("End time must be after start time!", "");
                return true;
            }
            return false;
        }
    }
}