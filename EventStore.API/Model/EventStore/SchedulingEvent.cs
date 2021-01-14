using System;
using EventStore.API.DTOs;

namespace EventStore.API.Model.EventStore
{
    public class SchedulingEvent : EventES
    {
        public SchedulingEventType EventType { get; set; }
        
        public Guid SchedulingSessionId { get; set; }
        public int UserAge { get; set; }
        public int UserId { get; set; }

        public SchedulingEvent()
        {
        }

        public SchedulingEvent(SchedulingEventDto schedulingEventDto)
        {
            EventType = schedulingEventDto.EventType;
            UserAge = schedulingEventDto.UserAge;
            UserId = schedulingEventDto.UserId;
            SchedulingSessionId = schedulingEventDto.SchedulingSessionId;
            TimeStamp = DateTime.Now;
        }
        
    }
}