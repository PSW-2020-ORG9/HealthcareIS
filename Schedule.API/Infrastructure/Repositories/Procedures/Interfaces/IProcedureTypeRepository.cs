using General.Repository;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Infrastructure.Repositories.Procedures.Interfaces
{
    public interface IProcedureTypeRepository : IWrappableRepository<ProcedureDetails, int>
    {
        ProcedureDetails GetPatientDefault();
    }
}