using System.Collections.Generic;
using User.API.Infrastructure;
using User.API.Model.Medication;
using User.API.Model.Users.Patients.MedicalHistory;


namespace User.API.Model.Schedule
{
    public class ExaminationReport : Entity<int>
    {
        public string Anamnesis { get; set; }

        public IEnumerable<Diagnosis> Diagnoses { get; set; }
        public IEnumerable<MedicationPrescription> Prescriptions { get; set; }
    }
}