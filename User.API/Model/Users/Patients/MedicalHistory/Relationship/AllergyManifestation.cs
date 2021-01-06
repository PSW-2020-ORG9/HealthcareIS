
namespace User.API.Model.Users.Patients.MedicalHistory.Relationship
{
    public class AllergyManifestation
    {
        public AllergyIntensity Intensity { get; set; }
        public int PatientId { get; set; }
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}