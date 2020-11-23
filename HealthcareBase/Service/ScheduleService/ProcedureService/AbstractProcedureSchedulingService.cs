using System;
using Model.CustomExceptions;
using Model.Notifications;
using Model.Schedule.Procedures;
using Service.ScheduleService.Validators;

namespace Service.ScheduleService.ProcedureService
{
    public abstract class AbstractProcedureSchedulingService<T> where T : Procedure
    {
        private readonly ProcedureValidator procedureValidator;
        private readonly ProcedureScheduleComplianceValidator scheduleValidator;
        private readonly TimeSpan timeLimit;

        protected AbstractProcedureSchedulingService(
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit)
        {
            this.scheduleValidator = scheduleValidator;
            this.procedureValidator = procedureValidator;
            this.timeLimit = timeLimit;
        }

        public abstract T GetByID(int id);
        protected abstract T Create(T procedure);
        protected abstract T Update(T procedure);
        protected abstract void Delete(T procedure);

        public T Schedule(T procedure)
        {
            if (procedure is null)
                throw new BadRequestException();
            ValidateForScheduling(procedure);
            var createdProcedure = Create(procedure);
            return createdProcedure;
        }

        public T Reschedule(T procedure)
        {
            if (procedure is null)
                throw new BadRequestException();
            ValidateForRescheduling(procedure);
            var updatedProcedure = Update(procedure);
            return updatedProcedure;
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
            ValidateTimeLimit(procedure);
            procedureValidator.ValidateProcedure(procedure);
            scheduleValidator.ValidateComplianceForScheduling(procedure);
            ValidateTimeLimit(procedure);
        }

        private void ValidateForRescheduling(Procedure procedure)
        {
            Procedure oldProcedure = GetByID(procedure.GetKey());
            ValidateTimeLimit(oldProcedure);
            ValidateTimeLimit(procedure);
            procedureValidator.ValidateProcedure(procedure);
            ValidateUpdateAllowed(oldProcedure, procedure);
            scheduleValidator.ValidateComplianceForRescheduling(procedure);
            ValidateTimeLimit(oldProcedure);
            ValidateTimeLimit(procedure);
        }

        private void ValidateUpdateAllowed(Procedure oldProcedure, Procedure newProcedure)
        {
            if (!oldProcedure.Patient.Equals(newProcedure.Patient))
                throw new BadRequestException();
            if (!oldProcedure.ProcedureType.Equals(newProcedure.ProcedureType))
                throw new BadRequestException();
            if (oldProcedure.ReferredFrom != null && !oldProcedure.ReferredFrom.Equals(newProcedure.ReferredFrom))
                throw new BadRequestException();
        }

        private void ValidateForCancelling(Procedure procedure)
        {
            ValidateTimeLimit(procedure);
        }

        private void ValidateTimeLimit(Procedure procedure)
        {
            if (procedure.TimeInterval.Start <= DateTime.Now + timeLimit)
                throw new TimingException();
        }
    }
}