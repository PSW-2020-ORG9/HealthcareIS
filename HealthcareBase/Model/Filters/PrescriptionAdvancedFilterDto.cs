using System;
using Model.Medication;

namespace HealthcareBase.Model.Filters
{
    public class PrescriptionAdvancedFilterDto : AbstractFilter<MedicationPrescription, int>
    {
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Diagnosis { get; set; }
        public TimeStatus Status { get; set; }

        protected override void ConfigureFilter()
        {
            if (!string.IsNullOrEmpty(Manufacturer))
                AddExpressionFunction(prescription => prescription.Medication.Manufacturer.Contains(Manufacturer));
            if (!string.IsNullOrEmpty(Name))
                AddExpressionFunction(prescription => prescription.Medication.Name.Contains(Name));
            if (!string.IsNullOrEmpty(Diagnosis))
                AddExpressionFunction(prescription => prescription.Diagnosis.Name.Contains(Diagnosis));
            switch (Status)
            {
                case TimeStatus.Past:
                    AddExpressionFunction(prescription => prescription.Instructions.EndDate < DateTime.Now);
                    break;
                case TimeStatus.Future:
                    AddExpressionFunction(prescription => prescription.Instructions.StartDate > DateTime.Now);
                    break;
                case TimeStatus.Ongoing:
                    AddExpressionFunction(prescription => 
                        prescription.Instructions.StartDate < DateTime.Now
                        && prescription.Instructions.EndDate > DateTime.Now);
                    break;
            }
        }
    }
}