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

        public SchedulingTimeDto GetSchedulingTimeStatistics()
        {
            var events = _schedulingEventRepository.Repository.GetAll();
            var timeNeededForEachScheduling = TimeNeededForEachScheduling(events);
            return new SchedulingTimeDto
            {
                MinSchedulingTime = timeNeededForEachScheduling.Min(),
                AvgSchedulingTime = new TimeSpan(Convert.ToInt32
                                    (timeNeededForEachScheduling.Average(e=>e.Ticks))),
                MaxSchedulingTime = timeNeededForEachScheduling.Max()
            };
        }

        private IEnumerable<TimeSpan> TimeNeededForEachScheduling(IEnumerable<SchedulingEvent> events)
        {
            var timings = new List<TimeSpan>();
            foreach (var sessionGuid in events.Select(e => e.SchedulingSessionId))
            {
                var sessionEvents = events
                    .Where(e => e.SchedulingSessionId.Equals(sessionGuid));
                if (!IsSessionFinished(sessionEvents))
                    timings.Add(GetTimeSpentInSession(sessionEvents));
                    
            }
            return timings;

        }

        public IDictionary<int, int> GetStepLeavingRate()
        {
            return StepLeavingRate(_schedulingEventRepository.Repository.GetAll());
        }

        private IDictionary<int, int> StepLeavingRate(IEnumerable<SchedulingEvent> events)
        {
            var leavingRate = InitStepsDictionary();
            foreach (var sessionGuid in events.Select(e => e.SchedulingSessionId))
            {
                var sessionEvents = events
                    .Where(e => e.SchedulingSessionId.Equals(sessionGuid));
                
                if (IsSessionFinished(sessionEvents))
                    leavingRate[(int) GetLastEventType(sessionEvents)] += 1;
            }

            return leavingRate;
        }

        private static bool IsSessionFinished(IEnumerable<SchedulingEvent> sessionEvents)
        {
            return sessionEvents.All(e => e.EventType != SchedulingEventType.FINISHED);
        }

        private IDictionary<int, int> InitStepsDictionary()
        {
            var stepsDictionary = new Dictionary<int,int>();
            foreach (var schedulingStep in SchedulingSteps())
                stepsDictionary[(int) schedulingStep] = 0;
            return stepsDictionary;
        }
        private static SchedulingEventType GetLastEventType(IEnumerable<SchedulingEvent> sessionEvents)
           => sessionEvents.OrderBy(e => e.TimeStamp)
               .Last().EventType;
        

        public IDictionary<int, TimeSpan> GetStepDurationStatistics()
            => AverageStepDuration(_schedulingEventRepository.Repository.GetAll(),
                SchedulingSteps());
        

        private static IEnumerable<SchedulingEventType> SchedulingSteps() =>
                Enum
                .GetValues(typeof(SchedulingEventType))
                .Cast<SchedulingEventType>()
                .Where(e => e != SchedulingEventType.FINISHED);

        private IDictionary<int, TimeSpan> AverageStepDuration(IEnumerable<SchedulingEvent> events, IEnumerable<SchedulingEventType> schedulingSteps)
        {
            var averageStepDuration = new Dictionary<int, TimeSpan>();

            foreach (var schedulingStep in schedulingSteps)
                averageStepDuration[(int) schedulingStep]
                    = GetAverageTimeSpent(GetTimesSpendOnStep(events, schedulingStep));

            return averageStepDuration;
        }

        private static List<TimeSpan> GetTimesSpendOnStep(IEnumerable<SchedulingEvent> events, SchedulingEventType schedulingStep)
        {
            var stepDuration = new List<TimeSpan>();

            foreach (var schedulingEvent in events.Where(e =>
                e.EventType.Equals(schedulingStep)))
            {
                var nextStep = NextStep(events, schedulingEvent);
                
                if (nextStep != null)
                    stepDuration.Add(nextStep.TimeStamp - schedulingEvent.TimeStamp);
            }

            return stepDuration;
        }

        private static SchedulingEvent NextStep(IEnumerable<SchedulingEvent> events, SchedulingEvent schedulingEvent)
        {
            return events
                .Where(e => e.SchedulingSessionId.Equals(schedulingEvent.SchedulingSessionId)
                            && e.EventType.Equals(schedulingEvent.EventType + 1)
                            && e.Id > schedulingEvent.Id)
                .OrderBy(e => e.TimeStamp).FirstOrDefault();
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
                var averageTime = GetAverageTimeByAge(events, distinctAge);
                if(averageTime.Ticks!=0)
                    times[distinctAge] = averageTime;
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

            return timesSpent.Count == 0 ? new TimeSpan(0) : GetAverageTimeSpent(timesSpent);
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
