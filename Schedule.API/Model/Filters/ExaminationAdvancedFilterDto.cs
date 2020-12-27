using System;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Model.Filters
{
    public class ExaminationAdvancedFilterDto : AbstractFilter<Examination, int>
    {
        public TimeStatus Status { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string ProcedureDetails { get; set; }
        public int DoctorSpecialtyId { get; set; }

        protected override void ConfigureFilter()
        {
            if (!string.IsNullOrEmpty(DoctorName))
                AddExpressionFunction(examination => examination.Doctor.Person.Name.Contains(DoctorName));
            if (!string.IsNullOrEmpty(DoctorSurname))
                AddExpressionFunction(examination => examination.Doctor.Person.Surname.Contains(DoctorSurname));
            if (!string.IsNullOrEmpty(ProcedureDetails))
                AddExpressionFunction(examination => examination.ExaminationReport.Anamnesis.Contains(ProcedureDetails));
            if (DoctorSpecialtyId != -1)
                AddExpressionFunction(examination => 
                    examination.RequiredSpecialtyId == DoctorSpecialtyId);
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