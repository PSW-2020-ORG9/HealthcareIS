using System;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public abstract class AbstractProcedureSchedulingService<T> where T : Procedure
    {
        protected AbstractProcedureSchedulingService() { }
        public abstract T GetByID(int id);
        protected abstract T Create(T procedure);
        protected abstract T Update(T procedure);
        protected abstract void ValidateProcedure(T procedure);

        public T Schedule(T procedure)
        {
            Validate(procedure);
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        private void Validate(T procedure)
        {
            ValidateProcedure(procedure);
            ValidateForScheduling(procedure);
        }

        private void ValidateForScheduling(Procedure procedure)
        {
            throw new NotImplementedException();
        }
    }
}