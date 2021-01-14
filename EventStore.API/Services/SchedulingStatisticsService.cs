using EventStore.API.DTOs;
using EventStore.API.Infrastructure.Repositories;
using EventStore.API.Model.EventStore;
using General.Repository;
using System;
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

        public StepsStatisticsDto GetStepsStatistics()
        {
            var sessionSteps = GetSessionSteps(_schedulingEventRepository.Repository.GetAll());
            
            return new StepsStatisticsDto
            {
                MinSteps = sessionSteps.Min(),
                AvgSteps = sessionSteps.Average(),
                MaxSteps = sessionSteps.Max()
            };
        }

        private List<int> GetSessionSteps(IEnumerable<SchedulingEvent> events)
        {
            var sessionSteps = new List<int>();
            foreach (Guid guid in events.Select(e => e.SchedulingSessionId).Distinct())
            {
                var session = events.Where(e => e.SchedulingSessionId == guid);
                if (IsSchedulingSessionSuccessful(session))
                {
                    sessionSteps.Add(session.Count());
                }
            }
            return sessionSteps;
        }

        public SuccessStatisticsDto GetSuccessStatistics()
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, TimeSpan> GetAgeStatistics()
        {
            var events = _schedulingEventRepository.Repository.GetAll();
            return GetAverageTimesWithAge(events);
        }

        private Dictionary<int, TimeSpan> GetAverageTimesWithAge(IEnumerable<SchedulingEvent> events)
        {
            var times = new Dictionary<int, TimeSpan>();
            foreach (int distinctAge in events.Select(e => e.UserAge).Distinct())
            {
                times[distinctAge] = GetAverageTimeByAge(events, distinctAge);
            }
            return times;
        }

        private TimeSpan GetAverageTimeByAge(IEnumerable<SchedulingEvent> events, int age)
        {
            var timesSpent = new List<TimeSpan>();
            var sessionIds = events.Where(e => e.UserAge == age).Select(e => e.SchedulingSessionId).Distinct();
            foreach (Guid guid in sessionIds)
            {
                var session = events.Where(e => e.SchedulingSessionId == guid);
                if (IsSchedulingSessionSuccessful(session))
                {
                    timesSpent.Add(GetTimeSpentInSession(session));
                }
            }
            return GetAverageTimeSpent(timesSpent);
        }

        private TimeSpan GetAverageTimeSpent(List<TimeSpan> timesSpent)
            => new TimeSpan(Convert.ToInt64(timesSpent.Average(ts => ts.Ticks)));

        private TimeSpan GetTimeSpentInSession(IEnumerable<SchedulingEvent> session)
        {
            var timeStamps = session.Select(s => s.TimeStamp);
            return timeStamps.Max() - timeStamps.Min();
        }

        private bool IsSchedulingSessionSuccessful(IEnumerable<SchedulingEvent> events)
            => events.Select(e => e.EventType).Contains(SchedulingEventType.FINISHED);

    }
}
