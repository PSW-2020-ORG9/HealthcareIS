using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Utilities;

namespace HospitalWebApp.Dtos
{
    public class RecommendationRequestDto
    {
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public TimeInterval TimeInterval { get; set; }
        public RecommendationPreference Preference { get; set; }
    }
}