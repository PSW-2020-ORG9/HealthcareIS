using General;
using General.Repository;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Infrastructure.Repositories.Procedures
{
    public class DiagnosisSqlRepository
        : GenericSqlRepository<Diagnosis, int>,
        IDiagnosisRepository
    {
        public DiagnosisSqlRepository(IContextFactory factory) : base(factory) {}
    }
}