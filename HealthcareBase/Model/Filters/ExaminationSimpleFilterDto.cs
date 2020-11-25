using Model.Schedule.Procedures;

namespace HealthcareBase.Model.Filters
{
    public class ExaminationSimpleFilterDto : AbstractFilter<Examination, int>
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