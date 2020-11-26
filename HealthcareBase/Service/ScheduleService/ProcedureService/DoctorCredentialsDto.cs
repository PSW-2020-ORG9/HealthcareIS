using HealthcareBase.Model.Filters;
using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class DoctorCredentialsDto : AbstractFilter<Examination, int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        protected override void ConfigureFilter()
        {
            if (!string.IsNullOrEmpty(Name)) 
                AddExpressionFunction(examination => examination.Doctor.Person.Name.Contains(Name));
            if (!string.IsNullOrEmpty(Surname))
                AddExpressionFunction(examination => examination.Doctor.Person.Surname.Contains(Surname));
        }
    }
}