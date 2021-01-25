using System;
using System.Collections.Generic;
using System.Linq;
using EventStore.API.DTOs;
using EventStore.API.Model.EventStore;

namespace EventStore.API.Services
{
    public class SchedulingStatisticCalculator : ISchedulingStatisticsCalculator
    {
        private IEnumerable<SchedulingEvent> _schedulingEvents;

        public SchedulingStatisticCalculator(IEnumerable<SchedulingEvent> schedulingEvents)
        {
            _schedulingEvents = schedulingEvents;
        }

        public SchedulingStatisticsDto GetSchedulingStatistics()
        {
            return new SchedulingStatisticsDto
            {
                AgeStatistics = GetAgeStatistics(),
                SchedulingTimeStatistics = GetSchedulingTimeStatistics(),
                StepDurationStatistics = GetStepDurationStatistics(),
                StepLeavingRateStatistics = GetStepLeavingRateStatistics(),
                StepsStatistics = GetStepsStatistics(),
                SuccessStatistics = GetSuccessStatistics()
            };
        }
        
        public StepsStatisticsDto GetStepsStatistics()
        {
            var sessionSteps = GetSessionSteps();
            
            return new StepsStatisticsDto
            {
                MinSteps = sessionSteps.Min(),
                AvgSteps = sessionSteps.Average(),
                MaxSteps = sessionSteps.Max()
            };
        }
        public IDictionary<int, TimeSpan> GetAgeStatistics()
        {
            return GetAverageTimesWithAge();
        }
        public SuccessStatisticsDto GetSuccessStatistics()
        {
  
            var sessionIds = _schedulingEvents
                .Select(e => e.SchedulingSessionId).Distinct();
            var successfulSessionIds = GetSuccessfulSessionIds(_schedulingEvents);

            return new SuccessStatisticsDto
            {
                SuccessCount = successfulSessionIds.Count(),
                FailureCount = sessionIds.Count() - successfulSessionIds.Count()
            };
        }
        public SchedulingTimeDto GetSchedulingTimeStatistics()
        {
            var timeNeededForEachScheduling = TimeNeededForEachScheduling();
            return new SchedulingTimeDto
            {
                MinSchedulingTime = timeNeededForEachScheduling.Min(),
                AvgSchedulingTime = new TimeSpan(Convert.ToInt32
                    (timeNeededForEachScheduling.Average(e=>e.Ticks))),
                MaxSchedulingTime = timeNeededForEachScheduling.Max()
            };
        }
        public IDictionary<int, int> GetStepLeavingRateStatistics()
        {
            var leavingRate = InitStepsDictionary();
            foreach (var sessionGuid in _schedulingEvents.Select(e => e.SchedulingSessionId).Distinct())
            {
                var sessionEvents = _schedulingEvents
                    .Where(e => e.SchedulingSessionId.Equals(sessionGuid));
                
                if (IsSessionUnfinished(sessionEvents))
                    leavingRate[(int) GetLastEventType(sessionEvents)] += 1;
            }

            return leavingRate;
        }
        public IDictionary<int, TimeSpan> GetStepDurationStatistics()
            => AverageStepDuration(SchedulingSteps());
        private IDictionary<int, TimeSpan> AverageStepDuration(IEnumerable<SchedulingEventType> schedulingSteps)
        {
            var averageStepDuration = new Dictionary<int, TimeSpan>();

            foreach (var schedulingStep in schedulingSteps)
                averageStepDuration[(int) schedulingStep]
                    = GetAverageTimeSpent(GetTimesSpendOnStep(schedulingStep));

            return averageStepDuration;
        }
        private List<TimeSpan> GetTimesSpendOnStep(SchedulingEventType schedulingStep)
        {
            var stepDuration = new List<TimeSpan>();

            foreach (var schedulingEvent in _schedulingEvents.Where(e =>
                e.EventType.Equals(schedulingStep)))
            {
                var nextStep = NextStep(schedulingEvent);
                
                if (nextStep != null)
                    stepDuration.Add(nextStep.TimeStamp - schedulingEvent.TimeStamp);
            }

            return stepDuration;
        }
        private SchedulingEvent NextStep(SchedulingEvent schedulingEvent)
        {
            return _schedulingEvents
                .Where(e => e.SchedulingSessionId.Equals(schedulingEvent.SchedulingSessionId)
                        && e.EventType.Equals(schedulingEvent.EventType + 1)
                        && e.Id > schedulingEvent.Id)
                .OrderBy(e => e.TimeStamp).FirstOrDefault();
        }
        private IDictionary<int, int> InitStepsDictionary()
        {
            var stepsDictionary = new Dictionary<int,int>();
            foreach (var schedulingStep in SchedulingSteps())
                stepsDictionary[(int) schedulingStep] = 0;
            return stepsDictionary;
        }
        private static IEnumerable<SchedulingEventType> SchedulingSteps() =>
            Enum
                .GetValues(typeof(SchedulingEventType))
                .Cast<SchedulingEventType>()
                .Where(e => e != SchedulingEventType.FINISHED);
        private static SchedulingEventType GetLastEventType(IEnumerable<SchedulingEvent> sessionEvents)
            => sessionEvents.OrderBy(e => e.TimeStamp)
                .Last().EventType;
        private static bool IsSessionUnfinished(IEnumerable<SchedulingEvent> sessionEvents)
        {
            return sessionEvents.All(e => e.EventType != SchedulingEventType.FINISHED);
        }
        
        
        private IEnumerable<TimeSpan> TimeNeededForEachScheduling()
        {
            var timings = new List<TimeSpan>();
            foreach (var sessionGuid in GetSuccessfulSessionIds(_schedulingEvents))
            {
                var sessionEvents = _schedulingEvents
                    .Where(e => e.SchedulingSessionId.Equals(sessionGuid));
                timings.Add(GetTimeSpentInSession(sessionEvents));
                    
            }
            return timings;

        }
        
        
        
        private List<int> GetSessionSteps()
        {
            var sessionSteps = new List<int>();
            foreach (Guid guid in GetSuccessfulSessionIds(_schedulingEvents))
            {
                var session = _schedulingEvents.Where(e => e.SchedulingSessionId == guid);
                sessionSteps.Add(session.Count());
            }
            return sessionSteps;
        }
        
        private Dictionary<int, TimeSpan> GetAverageTimesWithAge()
        {
            var times = new Dictionary<int, TimeSpan>();
            foreach (int distinctAge in _schedulingEvents.Select(e => e.UserAge).Distinct())
            {
                var averageTime = GetAverageTimeByAge(distinctAge);
                if(averageTime.Ticks!=0)
                    times[distinctAge] = averageTime;
            }
            return times;
        }
        private TimeSpan GetAverageTimeByAge(int age)
        {
            var timesSpent = new List<TimeSpan>();
            foreach (Guid guid in GetSuccessfulSessionIds(_schedulingEvents.Where(e => e.UserAge == age)))
            {
                var session = _schedulingEvents.Where(e => e.SchedulingSessionId == guid);
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
        private bool IsSchedulingSessionSuccessful(IEnumerable<SchedulingEvent> events)
            => events.Select(e => e.EventType).Contains(SchedulingEventType.FINISHED);
    }
}