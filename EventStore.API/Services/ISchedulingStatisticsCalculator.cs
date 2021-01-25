using System;
using System.Collections.Generic;
using EventStore.API.DTOs;

namespace EventStore.API.Services
{
    public interface ISchedulingStatisticsCalculator
    {
        public SchedulingStatisticsDto GetSchedulingStatistics();
        public StepsStatisticsDto GetStepsStatistics();
        public IDictionary<int, TimeSpan> GetAgeStatistics();
        public SuccessStatisticsDto GetSuccessStatistics();
        public SchedulingTimeDto GetSchedulingTimeStatistics();
        public IDictionary<int, int> GetStepLeavingRateStatistics();
        public IDictionary<int, TimeSpan> GetStepDurationStatistics();
    }
}