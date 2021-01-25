using EventStore.API.DTOs;
using EventStore.API.Infrastructure.Repositories;
using EventStore.API.Model.EventStore;
using General.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.API.Services
{
    public class SchedulingStatisticsService : ISchedulingStatisticsService
    {
        private readonly RepositoryWrapper<ISchedulingEventRepository> _schedulingEventRepository;

        public SchedulingStatisticsService(ISchedulingEventRepository schedulingEventRepository)
        {
            _schedulingEventRepository = new RepositoryWrapper<ISchedulingEventRepository>(schedulingEventRepository);
        }

        public SchedulingStatisticsDto GetStatistics()
        {
            var schedulingStatisticCalculator 
                = new SchedulingStatisticCalculator(_schedulingEventRepository.Repository.GetAll());
            return schedulingStatisticCalculator.GetSchedulingStatistics();
        }
    }
}
