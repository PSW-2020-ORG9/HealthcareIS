using Schedule.API.Model.Procedures;

namespace Schedule.API.Model.Filters
{
    public class ExaminationSimpleFilterDto : AbstractFilter<Examination, int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        protected override void ConfigureFilter()
        {
            if (!string.IsNullOrEmpty(Name)) 
                AddExpressionFunction(examination => examination.Doctor.Name.Contains(Name));
            if (!string.IsNullOrEmpty(Surname))
                AddExpressionFunction(examination => examination.Doctor.Surname.Contains(Surname));
        }
    }
}