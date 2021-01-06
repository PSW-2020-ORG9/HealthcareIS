using General;
using General.Repository;
using System.Linq;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;

namespace User.API.Infrastructure.Repositories.Users.UserAccounts
{
    public class PatientAccountSqlRepository : GenericSqlRepository<PatientAccount,int>,
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