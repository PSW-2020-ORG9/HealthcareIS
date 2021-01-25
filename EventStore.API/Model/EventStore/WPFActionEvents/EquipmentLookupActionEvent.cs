using EventStore.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Model.EventStore.WPFActionEvents
{
    public class EquipmentLookupActionEvent : WPFActionEvent
    {
        public int RoomId { get; set; }

        public EquipmentLookupActionEvent(EquipmentLookupDto equipmentLookupDto)
        {
            RoomId = equipmentLookupDto.RoomId;
            UserId = equipmentLookupDto.UserId;
            EventType = WPFActionEventType.EQUIPMENT_LOOKUP;
            TimeStamp = DateTime.Now;
        }

        public EquipmentLookupActionEvent() { }
    }
}
