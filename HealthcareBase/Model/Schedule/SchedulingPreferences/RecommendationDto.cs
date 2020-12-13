using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Model.Schedule.SchedulingPreferences
{
    public class RecommendationDto
    {
        public Doctor Doctor { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}