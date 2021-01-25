using System;
using System.Collections.Generic;
using EventStore.API.DTOs;
using EventStore.API.Model.EventStore;
using EventStore.API.Services;
using Shouldly;
using Xunit;

namespace EventStore.API.UnitTests
{
    public class SchedulingStatisticsTests
    {
        [Fact]
        public void Gets_age_statistics()
        {
            var expectedResult = new Dictionary<int,TimeSpan>
            {
                {18,new TimeSpan(0,0,0,12,500)}
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(AgeStatisticData());
            schedulingStatisticCalculator.GetAgeStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Gets_scheduling_time_statistics()
        {
            var expectedResult = new SchedulingTimeDto
            {
                AvgSchedulingTime = new TimeSpan(0, 0, 0, 15),
                MaxSchedulingTime = new TimeSpan(0, 0, 0, 20),
                MinSchedulingTime = new TimeSpan(0, 0, 0, 10)
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(TimeStatisticsData());
            schedulingStatisticCalculator.GetSchedulingTimeStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Gets_step_duration_statistics()
        {
            var expectedResult = new Dictionary<int,TimeSpan>
            {
                {0,new TimeSpan(0, 0, 0, 15)},
                {1,new TimeSpan(0, 0, 0, 15)},
                {2,new TimeSpan(0, 0, 0, 15)},
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(StepDurationStatisticsData());
            schedulingStatisticCalculator.GetStepDurationStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Gets_step_leaving_rate_statistics()
        {
            var expectedResult = new Dictionary<int,int>
            {
                {0,1},
                {1,0},
                {2,1}
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(StepLeavingStatisticsData());
            schedulingStatisticCalculator.GetStepLeavingRateStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Gets_step_statistics()
        {
            var expectedResult = new StepsStatisticsDto
            {
                MinSteps = 4,
                MaxSteps = 6,
                AvgSteps = 5
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(StepStatisticsData());
            schedulingStatisticCalculator.GetStepsStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Gets_success_ratio_statistics()
        {
            var expectedResult = new SuccessStatisticsDto
            {
                SuccessCount = 1,
                FailureCount = 1,
            };
            var schedulingStatisticCalculator = new SchedulingStatisticCalculator(SuccessRatioData());
            schedulingStatisticCalculator.GetSuccessStatistics().ShouldBeEquivalentTo(expectedResult);
        }

        public List<SchedulingEvent> SuccessRatioData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    Id=1,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=2,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=3,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=4,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 40),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=5,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=6,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=7,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 50),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
        public List<SchedulingEvent> StepStatisticsData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    Id=1,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=2,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=3,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=4,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 40),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=5,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 50),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=6,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 11, 00),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=7,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=8,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=9,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 50),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=10,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 11, 10),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
        private static List<SchedulingEvent> StepLeavingStatisticsData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    Id=1,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=2,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=3,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=5,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
        private static List<SchedulingEvent> StepDurationStatisticsData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    Id=1,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=2,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=3,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=4,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 40),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=5,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=6,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=7,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 50),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    Id=8,
                    TimeStamp = new DateTime(2010, 1, 1, 10, 11, 10),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
        private static List<SchedulingEvent> TimeStatisticsData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 13),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 16),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 15),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 30),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
        private static List<SchedulingEvent> AgeStatisticData()
        {
            return new List<SchedulingEvent>
            {
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 13),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 16),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("2daaf8dc-e38e-4ae9-8faf-2d3ac50149c6"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 10),
                    EventType = SchedulingEventType.STEP_1,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 15),
                    EventType = SchedulingEventType.STEP_2,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 20),
                    EventType = SchedulingEventType.STEP_3,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                },
                new SchedulingEvent
                {
                    TimeStamp = new DateTime(2010, 1, 1, 10, 10, 25),
                    EventType = SchedulingEventType.FINISHED,
                    SchedulingSessionId = Guid.Parse("46f62173-f7de-4668-9492-627fa4ee6070"),
                    UserAge = 18
                }
            };
        }
    }
}