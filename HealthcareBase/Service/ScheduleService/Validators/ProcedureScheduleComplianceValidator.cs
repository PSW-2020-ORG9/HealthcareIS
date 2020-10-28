using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ScheduleService.Validators
{
    public class ProcedureScheduleComplianceValidator
    {
        private CurrentScheduleContext context;

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
            IEnumerable<Renovation> conflictsWithRenovations = context.RenovationService.GetByRoomAndTime(procedure.Room, procedure.TimeInterval);
            if (conflictsWithRenovations.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithShifts(Procedure procedure)
        {
            IEnumerable<Shift> matchingShifts = context.ShiftService.GetByDoctorAndTimeContaining(procedure.Doctor, procedure.TimeInterval);
            if (matchingShifts.Count() == 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithProcedures(Procedure procedure, Action<IEnumerable<Procedure>> throwIfConflicts)
        {
            IEnumerable<Procedure> roomConflicts = context.ProcedureService.GetByRoomAndTime(procedure.Room, procedure.TimeInterval);
            throwIfConflicts(roomConflicts);
            IEnumerable<Procedure> doctorConflicts = context.ProcedureService.GetByDoctorAndTime(procedure.Doctor, procedure.TimeInterval);
            throwIfConflicts(doctorConflicts);
            IEnumerable<Procedure> patientConflicts = context.ProcedureService.GetByPatientAndTime(procedure.Patient, procedure.TimeInterval);
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
            else if (conflictList.Count() == 1 && conflictList.ToList()[0].Equals(procedure))
                return;
            else
                throw new ScheduleViolationException();
        }

        private Action<IEnumerable<Procedure>> GetThrowIfReschedulingConflicts(Procedure procedure)
        {
            return conflictList => ThrowIfReschedulingConflicts(procedure, conflictList);
        }
    }
}
