using System.Collections.Generic;
using System.Linq;
using General.Repository;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Model.Procedures;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Services.Procedures
{
    public class DiagnosisService : IDiagnosisService
    {
        private readonly RepositoryWrapper<IDiagnosisRepository> _diagnosisWrapper;

        public DiagnosisService(IDiagnosisRepository diagnosisRepository)
        {
            this._diagnosisWrapper = new RepositoryWrapper<IDiagnosisRepository>(diagnosisRepository);
        }

        public IEnumerable<Diagnosis> Find(IEnumerable<int> diagnosisIds)
            => _diagnosisWrapper.Repository.GetMatching(diagnosis => diagnosisIds.Contains(diagnosis.Id));

        public Diagnosis Find(int diagnosisId)
            => _diagnosisWrapper.Repository.GetMatching(diagnosis => diagnosis.Id == diagnosisId).FirstOrDefault();
    }
}