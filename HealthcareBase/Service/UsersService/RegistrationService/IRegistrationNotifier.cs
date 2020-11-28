using System;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public interface IRegistrationNotifier
    {
        public void SendActivationEmail(int patientId, string patientEmail, string emailTemplatePath); 
    }
}