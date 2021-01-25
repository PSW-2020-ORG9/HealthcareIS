using EventStore.API.Infrastructure.Repositories;
using EventStore.API.Model.EventStore;
using General.Repository;

namespace EventStore.API.Services
{
    public class SchedulingEventService : ISchedulingEventService
    {
        private readonly RepositoryWrapper<ISchedulingEventRepository> _schedulingEventRepository;

        public SchedulingEventService(ISchedulingEventRepository schedulingEventRepository)
        {
            _schedulingEventRepository = new RepositoryWrapper<ISchedulingEventRepository>(schedulingEventRepository);
        }

        public SchedulingEvent Record(SchedulingEvent schedulingEvent) 
            => _schedulingEventRepository.Repository.Create(schedulingEvent);
    }
}