using System;
using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.Schedule.Procedures;

namespace Service.ScheduleService.Validators
{
    public class ProcedureScheduleComplianceValidator
    {
        private readonly CurrentScheduleContext context;

        public ProcedureScheduleComplianceValidator(CurrentScheduleContext context)
        {
            this.context = context;
        }

        public void ValidateComplianceForScheduling(Procedure procedure)
        {
            ValidateComplianceWithRenovations(procedure);
            ValidateComplianceWithShifts(procedure);
            ValidateComplianceWithProcedures(procedure, ThrowIfSchedulingConflicts);
        }

        public void ValidateComplianceForRescheduling(Procedure procedure)
        {
            ValidateComplianceWithRenovations(procedure);
            ValidateComplianceWithShifts(procedure);
            ValidateComplianceWithProcedures(procedure, GetThrowIfReschedulingConflicts(procedure));
        }

        private void ValidateComplianceWithRenovations(Procedure procedure)
        {
            var conflictsWithRenovations =
                context.RenovationService.GetByRoomAndTime(procedure.Room, procedure.TimeInterval);
            if (conflictsWithRenovations.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithShifts(Procedure procedure)
        {
            var matchingShifts =
                context.ShiftService.GetByDoctorAndTimeContaining(procedure.Doctor, procedure.TimeInterval);
            if (matchingShifts.Count() == 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithProcedures(Procedure procedure,
            Action<IEnumerable<Procedure>> throwIfConflicts)
        {
            var roomConflicts = context.ProcedureService.GetByRoomAndTime(procedure.Room, procedure.TimeInterval);
            throwIfConflicts(roomConflicts);
            var doctorConflicts = context.ProcedureService.GetByDoctorAndTime(procedure.Doctor, procedure.TimeInterval);
            throwIfConflicts(doctorConflicts);
            var patientConflicts =
                context.ProcedureService.GetByPatientAndTime(procedure.Patient, procedure.TimeInterval);
            throwIfConflicts(patientConflicts);
        }

        private void ThrowIfSchedulingConflicts(IEnumerable<Procedure> conflictList)
        {
            if (conflictList.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ThrowIfReschedulingConflicts(Procedure procedure, IEnumerable<Procedure> conflictList)
        {
            if (conflictList.Count() == 0)
                return;
            if (conflictList.Count() == 1 && conflictList.ToList()[0].Equals(procedure))
                return;
            throw new ScheduleViolationException();
        }

        private Action<IEnumerable<Procedure>> GetThrowIfReschedulingConflicts(Procedure procedure)
        {
            return conflictList => ThrowIfReschedulingConflicts(procedure, conflictList);
        }
    }
}