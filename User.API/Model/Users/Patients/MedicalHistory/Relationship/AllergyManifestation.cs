using System.ComponentModel.DataAnnotations.Schema;

namespace User.API.Model.Users.Patients.MedicalHistory.Relationship
{
    public class AllergyManifestation
    {
        [Column(TypeName = "nvarchar(12)")]
        public AllergyIntensity Intensity { get; set; }
        
        public int PatientId { get; set; }
        
        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}