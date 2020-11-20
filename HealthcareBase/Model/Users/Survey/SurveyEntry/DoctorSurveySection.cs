using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.Employee;

namespace HealthcareBase.Model.Users.Survey.SurveyEntry
{
    public class DoctorSurveySection : RatedSurveySection
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}