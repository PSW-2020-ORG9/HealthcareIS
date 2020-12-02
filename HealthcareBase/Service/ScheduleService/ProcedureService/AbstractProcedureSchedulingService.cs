using System;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public abstract class AbstractProcedureSchedulingService<T> where T : Procedure
    {
        //private readonly ProcedureValidator procedureValidator;
        private readonly TimeSpan timeLimit;

        protected AbstractProcedureSchedulingService(
            //ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit)
        {
            //this.scheduleValidator = scheduleValidator;
            //this.procedureValidator = procedureValidator;
            this.timeLimit = timeLimit;
        }

        public abstract T GetByID(int id);
        protected abstract T Create(T procedure);
        protected abstract T Update(T procedure);
        protected abstract void Delete(T procedure);
        protected abstract void Validate(T procedure);

        protected abstract void ValidateProcedure(T procedure);

        public T Schedule(T procedure)
        {
            Validate(procedure);
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        public void Cancel(T procedure)
        {
            if (procedure is null)
                throw new BadRequestException();
            ValidateForCancelling(procedure);
            Delete(procedure);
        }

        private void ValidateForScheduling(Procedure procedure)
        {
            throw new NotImplementedException();
        }

        private void ValidateForCancelling(Procedure procedure)
        {
            throw new NotImplementedException();
        }
    }
}