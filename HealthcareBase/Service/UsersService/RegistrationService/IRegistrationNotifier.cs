using System;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public interface IRegistrationNotifier
    {
        public void SendActivationEmail(Guid patientGuid, string patientEmail, string emailTemplatePath); 
    }
}