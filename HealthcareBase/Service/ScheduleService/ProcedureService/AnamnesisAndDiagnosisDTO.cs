using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class AnamnesisAndDiagnosisDTO
    {
        public Examination Examination { get; set; }

        public string Anamnesis { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}