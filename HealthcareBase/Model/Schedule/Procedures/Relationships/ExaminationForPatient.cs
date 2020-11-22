using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Users.Patient.MedicalHistory;
using Repository.Generics;

namespace Model.Schedule.Procedures
{
    public class ExaminationForPatient
    {
        public int MedicalRecordId { get; set; }
        
        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
    }
}