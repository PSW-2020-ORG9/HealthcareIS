using EventStore.API.DTOs;
using System;
using System.Collections.Generic;

namespace EventStore.API.Services
{
    public interface ISchedulingStatisticsService
    {
        public SchedulingStatisticsDto GetStatistics();
    }
}
