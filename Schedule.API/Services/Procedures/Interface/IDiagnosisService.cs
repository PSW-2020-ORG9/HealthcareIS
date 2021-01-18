using System.Collections.Generic;
using Schedule.API.Model.Procedures;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IDiagnosisService
    {
        IEnumerable<Diagnosis> Find(IEnumerable<int> diagnosisIds);
        Diagnosis Find(int diagnosisId);
    }
}