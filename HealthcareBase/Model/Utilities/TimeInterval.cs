// File:    TimeInterval.cs
// Author:  Lana
// Created: 20 April 2020 23:38:16
// Purpose: Definition of Class TimeInterval

using System;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBase.Model.Utilities
{
    [Owned]
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
            throw new NotImplementedException();
        }
    }
}