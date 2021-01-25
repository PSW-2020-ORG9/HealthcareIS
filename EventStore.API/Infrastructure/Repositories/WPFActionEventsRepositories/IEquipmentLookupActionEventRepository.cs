using EventStore.API.Model.EventStore.WPFActionEvents;
using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.Infrastructure.Repositories
{
    public interface IEquipmentLookupActionEventRepository : IWrappableRepository<EquipmentLookupActionEvent, int>
    {
    }
}
