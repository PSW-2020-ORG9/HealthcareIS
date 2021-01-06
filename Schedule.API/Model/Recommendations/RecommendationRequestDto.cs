using Schedule.API.Model.Utilities;

namespace Schedule.API.Model.Recommendations
{
    public class RecommendationRequestDto
    {
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public TimeInterval TimeInterval { get; set; }
        public RecommendationPreference Preference { get; set; }
    }
}