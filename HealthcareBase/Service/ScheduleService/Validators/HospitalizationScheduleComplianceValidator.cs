using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ScheduleService.Validators
{
    public class HospitalizationScheduleComplianceValidator
    {
        private CurrentScheduleContext context;

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
            IEnumerable<Renovation> conflictsWithRenovations = context.RenovationService.GetByRoomAndTime(hospitalization.Room, hospitalization.TimeInterval);
            if (conflictsWithRenovations.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ValidateComplianceWithHospitlizations(Hospitalization hospitalization, Action<IEnumerable<Hospitalization>> throwIfConflicts)
        {
            IEnumerable<Hospitalization> patientConflicts =
                context.HospitalizationService.GetByPatientAndTime(hospitalization.Patient, hospitalization.TimeInterval);
            throwIfConflicts(patientConflicts);
            IEnumerable<Hospitalization> equipmentInUseConflicts =
                context.HospitalizationService.GetByEquipmentInUseAndTime(hospitalization.EquipmentInUse, hospitalization.TimeInterval);
            throwIfConflicts(equipmentInUseConflicts);
        }

        private void ThrowIfSchedulingConflicts(IEnumerable<Hospitalization> conflictList)
        {
            if (conflictList.Count() > 0)
                throw new ScheduleViolationException();
        }

        private void ThrowIfReschedulingConflicts(Hospitalization hospitalization, IEnumerable<Hospitalization> conflictList)
        {
            if (conflictList.Count() == 0)
                return;
            else if (conflictList.Count() == 1 && conflictList.ToList()[0].Equals(hospitalization))
                return;
            else
                throw new ScheduleViolationException();
        }

        private Action<IEnumerable<Hospitalization>> GetThrowIfReschedulingConflicts(Hospitalization hospitalization)
        {
            return conflictList => ThrowIfReschedulingConflicts(hospitalization, conflictList);
        }
    }
}
