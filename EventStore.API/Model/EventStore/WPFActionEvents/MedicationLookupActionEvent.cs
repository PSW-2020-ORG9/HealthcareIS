using EventStore.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Model.EventStore.WPFActionEvents
{
    public class MedicationLookupActionEvent :WPFActionEvent
    {
        public int RoomId { get; set; }

        public MedicationLookupActionEvent(MedicationLookupDto medicationLookupDto)
        {
            RoomId = medicationLookupDto.RoomId;
            UserId = medicationLookupDto.UserId;
            EventType = WPFActionEventType.MEDICATION_LOOKUP;
            TimeStamp = DateTime.Now;
        }

        public MedicationLookupActionEvent() { }
    }
}
