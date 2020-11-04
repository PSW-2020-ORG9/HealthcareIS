// File:    TimeInterval.cs
// Author:  Lana
// Created: 20 April 2020 23:38:16
// Purpose: Definition of Class TimeInterval

using Microsoft.EntityFrameworkCore;
using System;

namespace Model.Utilities
{
    [Owned]
    public class TimeInterval
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Duration => End - Start;

        public bool Overlaps(TimeInterval other)
        {
            if (other == null)
                return false;
            if (End <= other.Start)
                return false;
            if (other.End <= Start)
                return false;
            return true;
        }

        public bool Contains(TimeInterval other)
        {
            if (other == null)
                return false;
            return Start <= other.Start && End >= other.End;
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