using System;
using User.API.Model.Users.UserAccounts;
using User.API.Services.PatientService;

namespace User.API.Services.RegistrationService
{
    public class PatientRegistrationService
    {
        private readonly IPatientAccountService patientAccountService;
        private readonly IRegistrationNotifier registrationNotifier;

        public PatientRegistrationService(IPatientAccountService patientAccountService,IRegistrationNotifier registrationNotifier)
        {
            this.patientAccountService = patientAccountService;
            this.registrationNotifier = registrationNotifier;
        }

        public void RegisterPatient(PatientAccount patientAccount,string emailTemplatePath)
        {
            registrationNotifier
                .SendActivationEmail(patientAccount,
                    emailTemplatePath);
        }

        public void ActivatePatient(Guid guid)
        {
            patientAccountService.ActivateAccount(guid);
        }

    }
}