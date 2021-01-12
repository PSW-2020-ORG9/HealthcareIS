using System.Collections.Generic;
using System.Linq;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Model.Filters
{
    public abstract class AbstractExaminationFilter : AbstractFilter<Examination, int>
    {
        public IEnumerable<int> DoctorIds { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public int PatientId { get; set; }
        protected override void ConfigureFilter()
        {
            AddExpressionFunction(examination => DoctorIds.Contains(examination.DoctorId));
            AddExpressionFunction(examination => examination.PatientId == PatientId);
        }
    }
}