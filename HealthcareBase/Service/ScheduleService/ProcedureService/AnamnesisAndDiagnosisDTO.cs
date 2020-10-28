using Model.Miscellaneous;
using Model.Schedule.Procedures;

namespace Service.ScheduleService.ProcedureService
{
    public class AnamnesisAndDiagnosisDTO
    {
        public Examination Examination { get; set; }

        public string Anamnesis { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}