using EventStore.API.Model.EventStore.WPFActionEvents;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Infrastructure.Repositories.WPFActionEventsRepositories
{
    public interface IFloorChangeActionEventRepository : IWrappableRepository<FloorChangeActionEvent, int>
    {
    }
}
