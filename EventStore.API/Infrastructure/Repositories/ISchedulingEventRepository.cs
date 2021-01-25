using EventStore.API.Model.EventStore;
using General.Repository;

namespace EventStore.API.Infrastructure.Repositories
{
    public interface ISchedulingEventRepository : IWrappableRepository<SchedulingEvent,int>
    {
    }
}