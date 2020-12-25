


using User.API.Model.Users.UserAccounts;

namespace User.API.Services.RegistrationService
{
    public interface IRegistrationNotifier
    {
        public void SendActivationEmail(PatientAccount patientAccount, string emailTemplatePath); 
    }
}