using System;
using System.Collections.Generic;

namespace EventStore.API.DTOs
{
    public class SchedulingStatisticsDto
    {
        public StepsStatisticsDto StepsStatistics { get; set; }
        public IDictionary<int, TimeSpan> AgeStatistics { get; set; }
        public SuccessStatisticsDto SuccessStatistics { get; set; }
        public SchedulingTimeDto SchedulingTimeStatistics { get; set; }
        public IDictionary<int, int> StepLeavingRateStatistics { get; set; }
        public IDictionary<int, TimeSpan> StepDurationStatistics { get; set; }
    }
}