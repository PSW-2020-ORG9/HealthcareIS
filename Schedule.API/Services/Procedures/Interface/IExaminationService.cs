using System.Collections.Generic;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IExaminationService : IProcedureService<Examination>
    {
        public IEnumerable<Examination> Search(AbstractExaminationFilter filterDto);
        public IEnumerable<Examination> GetByPatientId(int patientId);
        public bool Cancel(int examinationId);
    }
}