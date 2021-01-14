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
            foreach (Guid guid in GetSuccessfulSessionIds(events))
            {
                var session = events.Where(e => e.SchedulingSessionId == guid);
                sessionSteps.Add(session.Count());
            }
            return sessionSteps;
        }

        public SuccessStatisticsDto GetSuccessStatistics()
        {
            var events = _schedulingEventRepository.Repository.GetAll();
            var sessionIds = events.Select(e => e.SchedulingSessionId).Distinct();
            var successfulSessionIds = GetSuccessfulSessionIds(events);

            return new SuccessStatisticsDto
            {
                SuccessCount = successfulSessionIds.Count(),
                FailureCount = sessionIds.Count() - successfulSessionIds.Count()
            };
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
            foreach (Guid guid in GetSuccessfulSessionIds(events.Where(e => e.UserAge == age)))
            {
                var session = events.Where(e => e.SchedulingSessionId == guid);
                timesSpent.Add(GetTimeSpentInSession(session));
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

        private IEnumerable<Guid> GetSuccessfulSessionIds(IEnumerable<SchedulingEvent> events)
        {
            var successfulSessionIds = new List<Guid>();
            foreach (Guid sessionId in events.Select(e => e.SchedulingSessionId).Distinct())
            {
                var session = events.Where(e => e.SchedulingSessionId == sessionId);
                if (IsSchedulingSessionSuccessful(session))
                {
                    successfulSessionIds.Add(sessionId);
                }
            }
            return successfulSessionIds;
        }
    }
}
