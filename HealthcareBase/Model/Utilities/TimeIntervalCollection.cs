// File:    TimeIntervalCollection.cs
// Author:  Lana
// Created: 02 June 2020 03:26:54
// Purpose: Definition of Class TimeIntervalCollection

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Utilities
{
    [Owned]
    public class TimeIntervalCollection
    {
        private List<TimeInterval> intervals;

        public TimeIntervalCollection()
        {
            intervals = new List<TimeInterval>();
        }

        public TimeIntervalCollection(IEnumerable<TimeInterval> intervals)
        {
            if (intervals is null)
                this.intervals = new List<TimeInterval>();
            else
                this.intervals = new List<TimeInterval>(intervals);
        }

        public TimeIntervalCollection(TimeIntervalCollection other)
        {
            if (other is null)
                intervals = new List<TimeInterval>();
            else
                intervals = new List<TimeInterval>(other.intervals);
        }

        public TimeIntervalCollection(TimeInterval interval)
        {
            intervals = new List<TimeInterval> {interval};
        }

        public IEnumerable<TimeInterval> Intervals => intervals;

        public TimeIntervalCollection Overlap(TimeIntervalCollection other)
        {
            var newIntervals = new List<TimeInterval>();

            foreach (var interval1 in intervals)
            foreach (var interval2 in intervals)
                if (!interval1.Overlaps(interval2))
                {
                }
                else if (interval1.Contains(interval2))
                {
                    newIntervals.Add(interval2);
                }
                else if (interval2.Contains(interval1))
                {
                    newIntervals.Add(interval1);
                }
                else if (interval1.Start < interval2.Start)
                {
                    if (!interval1.End.Equals(interval2.Start))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = interval2.Start,
                            End = interval1.End
                        });
                    if (!interval2.End.Equals(interval1.Start))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = interval1.Start,
                            End = interval2.End
                        });
                }

            intervals = newIntervals;
            return this;
        }

        public TimeIntervalCollection SubtractInterval(TimeInterval interval)
        {
            var newIntervals = new List<TimeInterval>();

            foreach (var currentInterval in intervals)
                if (!currentInterval.Overlaps(interval))
                {
                    newIntervals.Add(currentInterval);
                }
                else if (interval.Contains(currentInterval))
                {
                }
                else if (currentInterval.Contains(interval))
                {
                    if (!currentInterval.Start.Equals(interval.Start))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = currentInterval.Start,
                            End = interval.Start
                        });
                    if (!currentInterval.End.Equals(interval.End))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = interval.End,
                            End = currentInterval.End
                        });
                }
                else if (currentInterval.Start > interval.Start)
                {
                    if (!currentInterval.End.Equals(interval.End))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = interval.End,
                            End = currentInterval.End
                        });
                }
                else
                {
                    if (!currentInterval.Start.Equals(interval.Start))
                        newIntervals.Add(new TimeInterval
                        {
                            Start = currentInterval.Start,
                            End = interval.Start
                        });
                }

            intervals = newIntervals;
            return this;
        }

        public TimeIntervalCollection RemoveEarlier(DateTime cutoff)
        {
            intervals = intervals.Where(interval => interval.End <= cutoff).ToList();
            foreach (var interval in intervals)
                if (interval.Start < cutoff)
                    interval.Start = cutoff;
            return this;
        }

        public TimeIntervalCollection Filter(TimeSpan minimumLength)
        {
            intervals = intervals.Where(interval => interval.Duration < minimumLength).ToList();
            return this;
        }
    }
}