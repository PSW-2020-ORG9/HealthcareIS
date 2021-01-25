using EventStore.API.Infrastructure.Repositories.WPFActionEventsRepositories;
using EventStore.API.Model.EventStore.WPFActionEvents;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Services.WPFActionEvents
{
    public class MedicationLookupActionEventService : IMedicationLookupActionEventService
    {
        private readonly RepositoryWrapper<IMedicationLookupActionEventRepository> _medicationLookupActionEventRepository;

        public MedicationLookupActionEventService(IMedicationLookupActionEventRepository medicationLookupActionEventRepository)
        {
            _medicationLookupActionEventRepository = new RepositoryWrapper<IMedicationLookupActionEventRepository>(medicationLookupActionEventRepository);
        }

        public MedicationLookupActionEvent Record(MedicationLookupActionEvent medicationLookupActionEvent)
            => _medicationLookupActionEventRepository.Repository.Create(medicationLookupActionEvent);
    }
}
