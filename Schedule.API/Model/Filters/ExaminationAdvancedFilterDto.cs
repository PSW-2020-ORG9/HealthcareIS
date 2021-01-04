using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Model.Filters
{
    public class ExaminationAdvancedFilterDto : AbstractExaminationFilter
    {
        public TimeStatus Status { get; set; }
        public string ProcedureDetails { get; set; }
        public int DoctorSpecialtyId { get; set; }

        protected override void ConfigureFilter()
        {
            base.ConfigureFilter();
            
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