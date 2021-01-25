using EventStore.API.Infrastructure.Repositories.WPFActionEventsRepositories;
using EventStore.API.Model.EventStore.WPFActionEvents;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Services.WPFActionEvents
{
    public class FloorChangeActionEventService : IFloorChangeActionEventService
    {
        private readonly RepositoryWrapper<IFloorChangeActionEventRepository> _floorChangeActionEventRepository;

        public FloorChangeActionEventService(IFloorChangeActionEventRepository floorChangeActionEventRepository)
        {
            _floorChangeActionEventRepository = new RepositoryWrapper<IFloorChangeActionEventRepository>(floorChangeActionEventRepository);
        }

        public FloorChangeActionEvent Record(FloorChangeActionEvent floorChangeActionEvent)
            => _floorChangeActionEventRepository.Repository.Create(floorChangeActionEvent);
    }
}
