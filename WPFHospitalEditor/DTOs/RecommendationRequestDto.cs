using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class RecommendationRequestDto
    {
        public int DoctorId { get; internal set; }
        public int SpecialtyId { get; internal set; }
        public TimeInterval TimeInterval { get; internal set; }
        public RecommendationPreference Preference { get; internal set; }
    }
}
