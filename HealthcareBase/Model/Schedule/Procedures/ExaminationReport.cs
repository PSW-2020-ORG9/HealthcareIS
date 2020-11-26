using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Procedures
{
    public class ExaminationReport : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Anamnesis { get; set; }

        public IEnumerable<Diagnosis> Diagnoses { get; set; }
        public IEnumerable<MedicationPrescription> Prescriptions { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}