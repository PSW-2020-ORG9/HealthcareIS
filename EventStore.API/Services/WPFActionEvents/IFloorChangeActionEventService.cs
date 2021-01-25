using EventStore.API.Model.EventStore.WPFActionEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Services.WPFActionEvents
{
    public interface IFloorChangeActionEventService
    {
        FloorChangeActionEvent Record(FloorChangeActionEvent floorChangeActionEvent);
    }
}
