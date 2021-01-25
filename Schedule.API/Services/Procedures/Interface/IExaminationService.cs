using System.Collections.Generic;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IExaminationService : IProcedureService<Examination>
    {
        IEnumerable<Examination> Search(AbstractExaminationFilter filterDto);
        IEnumerable<Examination> GetByPatientId(int patientId);
        IEnumerable<Examination> GetBySpecialtyId(int patientId);
        bool Cancel(int examinationId);
        IEnumerable<Examination> GetByRoomId(int roomId);
    }
}