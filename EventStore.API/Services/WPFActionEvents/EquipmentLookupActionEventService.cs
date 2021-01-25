using EventStore.API.Infrastructure.Repositories;
using EventStore.API.Model.EventStore.WPFActionEvents;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Services.WPFActionEvents
{
    public class EquipmentLookupActionEventService : IEquipmentLookupActionEventService
    {
        private readonly RepositoryWrapper<IEquipmentLookupActionEventRepository> _equipmentLookupActionEventRepository;

        public EquipmentLookupActionEventService(IEquipmentLookupActionEventRepository equipmentLookupActionEventRepository)
        {
            _equipmentLookupActionEventRepository = new RepositoryWrapper<IEquipmentLookupActionEventRepository>(equipmentLookupActionEventRepository);
        }

        public EquipmentLookupActionEvent Record(EquipmentLookupActionEvent equipmentLookupActionEvent)
            => _equipmentLookupActionEventRepository.Repository.Create(equipmentLookupActionEvent);
    }
}
