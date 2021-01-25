using System;

namespace EventStore.API.DTOs
{
    public class SchedulingTimeDto
    {
        public TimeSpan MinSchedulingTime { get; set; }
        public TimeSpan AvgSchedulingTime { get; set; }
        public TimeSpan MaxSchedulingTime { get; set; }
    }
}