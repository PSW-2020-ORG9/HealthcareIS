using System;
using System.ComponentModel.DataAnnotations;
using General;

namespace EventStore.API.Model.EventStore
{
    public class EventES : Entity<int>
    {
        public DateTime TimeStamp { get; set; }
    }
}
