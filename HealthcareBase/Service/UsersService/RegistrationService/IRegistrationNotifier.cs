using System;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public interface IRegistrationNotifier
    {
        public void SendActivationEmail(Guid guid, string patientEmail, string emailTemplatePath); 
    }
}