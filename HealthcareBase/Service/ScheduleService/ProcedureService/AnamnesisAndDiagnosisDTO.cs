using Model.Miscellaneous;
using Model.Schedule.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ScheduleService.ProcedureService
{
    public class AnamnesisAndDiagnosisDTO
    {
        private Examination examination;
        private String anamnesis;
        private Diagnosis diagnosis;

        public Examination Examination { get => examination; set => examination = value; }
        public string Anamnesis { get => anamnesis; set => anamnesis = value; }
        public Diagnosis Diagnosis { get => diagnosis; set => diagnosis = value; }
    }
}
