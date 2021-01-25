using EventStore.API.Model.EventStore.WPFActionEvents;
using General;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Infrastructure.Repositories.WPFActionEventsRepositories
{
    public class MedicationLookupActionEventSqlRepository : GenericSqlRepository<MedicationLookupActionEvent, int>, IMedicationLookupActionEventRepository
    {
        public MedicationLookupActionEventSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {

        }
    }
}
