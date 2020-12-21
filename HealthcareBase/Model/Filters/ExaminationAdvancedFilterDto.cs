using System;
using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Model.Filters
{
    public class ExaminationAdvancedFilterDto : AbstractFilter<Examination, int>
    {
        public TimeStatus Status { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string ProcedureDetails { get; set; }
        public string DoctorSpecialty { get; set; }

        protected override void ConfigureFilter()
        {
            if (!string.IsNullOrEmpty(DoctorName))
                AddExpressionFunction(examination => examination.Doctor.Person.Name.Contains(DoctorName));
            if (!string.IsNullOrEmpty(DoctorSurname))
                AddExpressionFunction(examination => examination.Doctor.Person.Surname.Contains(DoctorSurname));
            if (!string.IsNullOrEmpty(ProcedureDetails))
                AddExpressionFunction(examination => examination.ProcedureDetails.Description.Contains(ProcedureDetails));
            if (!string.IsNullOrEmpty(DoctorSpecialty))
                AddExpressionFunction(examination => 
                    examination.ProcedureDetails.RequiredSpecialty.Name.Contains(DoctorSpecialty));
            switch (Status)
            {
                case TimeStatus.Past:
                    AddExpressionFunction(examination => examination.TimeInterval.End < DateTime.Now);
                    break;
                case TimeStatus.Future:
                    AddExpressionFunction(examination => examination.TimeInterval.Start > DateTime.Now);
                    break;
            }
            
        }
    }
}