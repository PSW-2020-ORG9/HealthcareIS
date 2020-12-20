using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Hospitalizations;

namespace HealthcareBase.Service.ScheduleService.Validators
{
    public class HospitalizationScheduleComplianceValidator
    {
        private readonly CurrentScheduleContext context;

        public HospitalizationScheduleComplianceValidator(CurrentScheduleContext context)
        {
            this.context = context;
        }

        public void ValidateComplianceForScheduling(Hospitalization hospitalization)
        {
            ValidateComplianceWithRenovations(hospitalization);
            ValidateComplianceWithHospitlizations(hospitalization, ThrowIfSchedulingConflicts);
        }

        public void ValidateComplianceForRescheduling(Hospitalization hospitalization)
        {
            ValidateComplianceWithRenovations(hospitalization);
            ValidateComplianceWithHospitlizations(hospitalization, GetThrowIfReschedulingConflicts(hospitalization));
        }

        private void ValidateComplianceWithRenovations(Hospitalization hospitalization)
        {
            var conflictsWithRenovations =
                context.RenovationService.GetByRoomAndTime(hospitalization.Room, hospitalization.TimeInterval);
            if (conflictsWithRenovations.Any())
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithHospitlizations(Hospitalization hospitalization,
            Action<IEnumerable<Hospitalization>> throwIfConflicts)
        {
            var patientConflicts =
                context.HospitalizationService.GetByPatientAndTime(hospitalization.Patient,
                    hospitalization.TimeInterval);
            throwIfConflicts(patientConflicts);
        }

        private void ThrowIfSchedulingConflicts(IEnumerable<Hospitalization> conflictList)
        {
            if (conflictList.Any())
                throw new ScheduleViolationException();
        }

        private void ThrowIfReschedulingConflicts(Hospitalization hospitalization,
            IEnumerable<Hospitalization> conflictList)
        {
            if (!conflictList.Any())
                return;
            if (conflictList.Count() == 1 && conflictList.ToList()[0].Equals(hospitalization))
                return;
            throw new ScheduleViolationException();
        }

        private Action<IEnumerable<Hospitalization>> GetThrowIfReschedulingConflicts(Hospitalization hospitalization)
        {
            return conflictList => ThrowIfReschedulingConflicts(hospitalization, conflictList);
        }
    }
}