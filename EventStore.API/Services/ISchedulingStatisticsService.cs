using EventStore.API.DTOs;
using System;
using System.Collections.Generic;

namespace EventStore.API.Services
{
    public interface ISchedulingStatisticsService
    {
        StepsStatisticsDto GetStepsStatistics();
        IDictionary<int, TimeSpan> GetAgeStatistics();
        SuccessStatisticsDto GetSuccessStatistics();
        IDictionary<int, TimeSpan> GetStepDurationStatistics();
    }
}
