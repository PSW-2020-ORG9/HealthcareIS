using System;
using EventStore.API.DTOs;

namespace EventStore.API.Model.EventStore
{
    public class SchedulingEvent : EventES
    {
        public SchedulingEventType EventType { get; set; }
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
            TimeStamp = DateTime.Now;
        }
        
    }
}