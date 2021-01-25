using EventStore.API.Model.EventStore.WPFActionEvents;
using General;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Infrastructure.Repositories.WPFActionEventsRepositories
{
    public class EquipmentLookupActionEventSqlRepository : GenericSqlRepository<EquipmentLookupActionEvent, int>, IEquipmentLookupActionEventRepository
    {
        public EquipmentLookupActionEventSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {

        }
    }
}
