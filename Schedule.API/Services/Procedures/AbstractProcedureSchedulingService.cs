using General.Repository;
using Schedule.API.Infrastructure.Repositories;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Exceptions;
using Schedule.API.Model.Procedures;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Services.Procedures
{
    public abstract class AbstractProcedureSchedulingService<T> where T : Procedure
    {
        protected readonly RepositoryWrapper<IShiftRepository> _shiftWrapper;

        protected AbstractProcedureSchedulingService(IShiftRepository shiftRepository)
        {
            _shiftWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);
        }
        
        protected AbstractProcedureSchedulingService() { }

        public abstract T GetByID(int id);

        public T Schedule(T procedure)
        {
            Validate(procedure);
            LinkRoomToProcedure(procedure);
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        public T ScheduleEmergency(T procedure)
        {
            ValidateProcedure(procedure);
            LinkRoomToProcedure(procedure);
            procedure.Priority = ProcedurePriority.High;
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        protected abstract T Create(T procedure);
        protected abstract T Update(T procedure);
        protected abstract void ValidateProcedure(T procedure);
        protected abstract void ValidateForScheduling(T procedure);
        
        private void Validate(T procedure)
        {
            ValidateProcedure(procedure);
            ValidateForScheduling(procedure);
        }

        private void LinkRoomToProcedure(Procedure procedure)
        {
            var shiftRoomId = _shiftWrapper.Repository.GetAssignedRoomId(
                procedure.DoctorId, procedure.TimeInterval.Start.Date
            );
            if (shiftRoomId == -1)
                throw new ScheduleViolationException($"No shifts available for the given doctor.");
            
            procedure.RoomId = shiftRoomId;
        }
    }
}