using System;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Service.UsersService.PatientService
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