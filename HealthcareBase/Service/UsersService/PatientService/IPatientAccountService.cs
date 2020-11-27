using System;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Model.Users.UserFeedback;

namespace HealthcareBase.Service.UsersService.PatientService
{
    public interface IPatientAccountService
    {
        public void CreateAccount(PatientAccount patientAccount);
        public void DeleteAccount(PatientAccount patientAccount);
        public PatientAccount GetAccount(Patient patient);
        public PatientAccount ChangePassword(PatientAccount account, string newPassword);
        public PatientAccount AddFavouriteDoctor(Doctor doctor, PatientAccount account);
        public PatientAccount RemoveFavoriteDoctor(Doctor doctor, PatientAccount account);
        public void RecordSurveyResponse(PatientSurveyResponse surveyResponse);
        public void ActivateAccount(Guid guid);
    }
}