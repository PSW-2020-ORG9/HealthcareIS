using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Model.EventStore
{
    public class WPFActionEvent : EventES
    {
        public WPFActionEventType EventType { get; set; }

        public int UserId { get; set; }
    }
}
