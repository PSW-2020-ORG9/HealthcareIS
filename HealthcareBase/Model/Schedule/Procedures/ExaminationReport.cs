using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Medication;
using Model.Miscellaneous;
using Repository.Generics;

namespace Model.Users.Patient.MedicalHistory
{
    public class ExaminationReport : Entity<int>
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