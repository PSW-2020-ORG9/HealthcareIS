using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Service.ScheduleService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.HospitalResourcesService.Validators
{
    public class RenovationScheduleComplianceValidator
    {
        private CurrentScheduleContext context;

        public RenovationScheduleComplianceValidator(CurrentScheduleContext context)
        {
            this.context = context;
        }

        public void ValidateComplianceForScheduling(Renovation renovation)
        {
            ValidateComplianceWithHospitalizations(renovation);
            ValidateComplianceWithProcedures(renovation);
            ValidateComplianceWithRenovations(renovation, ThrowIfSchedulingConflicts);
        }

        public void ValidateComplianceForRescheduling(Renovation renovation)
        {
            ValidateComplianceWithHospitalizations(renovation);
            ValidateComplianceWithProcedures(renovation);
            ValidateComplianceWithRenovations(renovation, GetThrowIfReschedulingConflicts(renovation));
        }

        private void ValidateComplianceWithHospitalizations(Renovation renovation)
        {
            IEnumerable<Hospitalization> hospitalisationConflicts =
                context.HospitalizationService.GetByRoomAndTime(renovation.Room, renovation.TimeInterval);
            if (hospitalisationConflicts.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithProcedures(Renovation renovation)
        {
            IEnumerable<Procedure> procedureConflicts = context.ProcedureService.GetByRoomAndTime(renovation.Room, renovation.TimeInterval);
            if (procedureConflicts.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithRenovations(Renovation renovation, Action<IEnumerable<Renovation>> throwIfConflicts)
        {
            IEnumerable<Renovation> conflictsWithRenovations = context.RenovationService.GetByRoomAndTime(renovation.Room, renovation.TimeInterval);
            throwIfConflicts(conflictsWithRenovations);
        }

        private void ThrowIfSchedulingConflicts(IEnumerable<Renovation> conflictList)
        {
            if (conflictList.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ThrowIfReschedulingConflicts(Renovation renovation, IEnumerable<Renovation> conflictList)
        {
            if (conflictList.Count() == 0)
                return;
            else if (conflictList.Count() == 1 && conflictList.ToList()[0].Equals(renovation))
                return;
            else
                throw new ScheduleViolationException();
        }

        private Action<IEnumerable<Renovation>> GetThrowIfReschedulingConflicts(Renovation renovation)
        {
            return conflictList => ThrowIfReschedulingConflicts(renovation, conflictList);
        }
    }
}
