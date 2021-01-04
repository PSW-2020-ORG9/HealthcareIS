using System;
using User.API.Model.Users.UserAccounts;

namespace User.API.Services.RegistrationService
{
    public interface IPatientRegistrationService
    {
        void RegisterPatient(PatientAccount patientAccount, string emailTemplatePath);
        void ActivatePatient(Guid guid);
    }
}
