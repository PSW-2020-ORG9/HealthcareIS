using System;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public interface IRegistrationNotifier
    {
        public void SendActivationEmail(PatientAccount patientAccount, string emailTemplatePath); 
    }
}