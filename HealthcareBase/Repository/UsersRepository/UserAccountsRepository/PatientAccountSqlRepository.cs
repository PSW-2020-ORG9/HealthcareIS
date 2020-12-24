using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;
using System.Linq;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public class PatientAccountSqlRepository : 
        GenericSqlRepository<PatientAccount,int>,
        IPatientAccountRepository
    {
        public PatientAccountSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public PatientAccount GetByPatient(Patient patient)
            => GetByPatientId(patient.Id);

        public PatientAccount GetByPatientId(int id)
            => GetMatching(patientAccount => patientAccount.PatientId == id).FirstOrDefault();
    }
}