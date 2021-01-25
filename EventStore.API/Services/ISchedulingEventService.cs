using EventStore.API.Model.EventStore;

namespace EventStore.API.Services
{
    public interface ISchedulingEventService
    {
        SchedulingEvent Record(SchedulingEvent schedulingEvent);
    }
}