using System;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public abstract class AbstractProcedureSchedulingService<T> where T : Procedure
    {
        private readonly RepositoryWrapper<IShiftRepository> _shiftWrapper;

        public AbstractProcedureSchedulingService(IShiftRepository shiftRepository)
        {
            _shiftWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);
        }
        
        protected AbstractProcedureSchedulingService() { }
        public abstract T GetByID(int id);
        protected abstract T Create(T procedure);
        protected abstract T Update(T procedure);
        protected abstract void ValidateProcedure(T procedure);
        protected abstract void ValidateForScheduling(T procedure);

        public T Schedule(T procedure)
        {
            Validate(procedure);
            LinkRoomToProcedure(procedure);
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        private void Validate(T procedure)
        {
            ValidateProcedure(procedure);
            ValidateForScheduling(procedure);
        }

        private void LinkRoomToProcedure(Procedure procedure)
        {
            procedure.RoomId = _shiftWrapper.Repository.GetAssignedRoomId(
                procedure.DoctorId, procedure.TimeInterval.Start.Date
            );
        }
    }
}