// File:    TimeInterval.cs
// Author:  Lana
// Created: 20 April 2020 23:38:16
// Purpose: Definition of Class TimeInterval

using System;

namespace Model.Utilities
{
    public class TimeInterval
    {
        private DateTime start;
        private DateTime end;

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public TimeSpan Duration { get { return end - start; } }

        public Boolean Overlaps(TimeInterval other)
        {
            if (other == null)
                return false;
            if (end <= other.start)
                return false;
            if (other.end <= start)
                return false;
            return true;
        }

        public Boolean Contains(TimeInterval other)
        {
            if (other == null)
                return false;
            return start <= other.start && end >= other.end;
        }

        public override bool Equals(object obj)
        {
            return obj is TimeInterval interval &&
                   Start.Equals(interval.Start) &&
                   End.Equals(interval.End);
        }

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + Start.GetHashCode();
            hashCode = hashCode * -1521134295 + End.GetHashCode();
            return hashCode;
        }
    }
}