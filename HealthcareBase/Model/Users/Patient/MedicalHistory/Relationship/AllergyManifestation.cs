using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Miscellaneous;

namespace HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship
{
    public class AllergyManifestation
    {
        [Column(TypeName = "nvarchar(12)")]
        public AllergyIntensity Intensity { get; set; }
        
        public int MedicalRecordId { get; set; }
        
        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}