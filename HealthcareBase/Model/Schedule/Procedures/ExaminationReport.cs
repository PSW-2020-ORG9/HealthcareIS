using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class ExaminationReport : Entity<int>
    {
        public string Anamnesis { get; set; }

        public IEnumerable<Diagnosis> Diagnoses { get; set; }
        public IEnumerable<MedicationPrescription> Prescriptions { get; set; }
    }
}