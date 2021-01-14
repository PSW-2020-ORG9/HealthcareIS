using System;
using EventStore.API.Model.EventStore;

namespace EventStore.API.DTOs
{
    public class SchedulingEventDto
    {
        public SchedulingEventType EventType { get; set; }
        public int UserAge { get; set; }
        public Guid SchedulingSessionId { get; set; }
        
        public int UserId { get; set; }
    }
}