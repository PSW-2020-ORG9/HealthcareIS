using EventStore.API.Model.EventStore;
using General;
using General.Repository;

namespace EventStore.API.Infrastructure.Repositories
{
    public class SchedulingEventSqlRepository : GenericSqlRepository<SchedulingEvent, int>, ISchedulingEventRepository
    {
        public SchedulingEventSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}