using System.Collections.Generic;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IDiagnosisService
    {
        public IEnumerable<Diagnosis> Find(IEnumerable<int> diagnosisIds);
        public Diagnosis Find(int diagnosisId);
    }
}