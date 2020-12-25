using System;
using User.API.Model.Users.Patients;
using User.API.Model.Users.UserAccounts;

namespace User.API.Services.PatientService
{
    public interface IPatientAccountService
    {
        public PatientAccount CreateAccount(PatientAccount patientAccount);
        public void DeleteAccount(PatientAccount patientAccount);
        public PatientAccount GetAccount(Patient patient);
        public PatientAccount GetAccount(int patientId);
        public PatientAccount ChangePassword(PatientAccount account, string newPassword);
        public void ActivateAccount(Guid guid);
    }
}