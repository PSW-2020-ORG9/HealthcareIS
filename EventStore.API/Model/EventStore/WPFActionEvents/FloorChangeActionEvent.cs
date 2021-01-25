using EventStore.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Model.EventStore.WPFActionEvents
{
    public class FloorChangeActionEvent : WPFActionEvent
    {
        public int BuildingId { get; set; }
        public int FloorId { get; set; }

        public FloorChangeActionEvent(FloorChangeDto floorChangeDto)
        {
            UserId = floorChangeDto.UserId;
            BuildingId = floorChangeDto.BuildingId;
            FloorId = floorChangeDto.FloorId;
            EventType = WPFActionEventType.FLOOR_CHANGED;
            TimeStamp = DateTime.Now;
        }

        public FloorChangeActionEvent() { }
    }
}
