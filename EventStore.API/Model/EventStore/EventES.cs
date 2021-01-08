using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventStore.Model.EventStore
{
    public class EventES
    {
        [Key]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public EventType Type { get; set; }

        public string EventData { get; set; }
    }
}
