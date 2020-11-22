using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.Patient.MedicalHistory;
using Repository.Generics;

namespace Model.Schedule.Procedures
{
    public class SurgeryForPatient
    {
        public int MedicalRecordId { get; set; }
        
        [ForeignKey("Surgery")] 
        public int SurgeryId { get; set; }
        public Surgery Surgery { get; set; }
    }
}