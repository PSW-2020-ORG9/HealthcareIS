using HealthcareBase.Model.Database;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.UserAccountsRepository
{
    public class PatientAccountSqlRepository:GenericSqlRepository<PatientAccount,int>,IPatientAccountRepository
    {
        public PatientAccountSqlRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        public bool ExistsByJMBG(string jmbg)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUsernameUnique(string username)
        {
            throw new System.NotImplementedException();
        }

        public string GetPasswordByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public PatientAccount GetByUsernameAndPassword(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public PatientAccount GetByPatient(Patient patient)
        {
            throw new System.NotImplementedException();
        }
    }
}