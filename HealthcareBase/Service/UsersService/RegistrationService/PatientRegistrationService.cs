using System;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Service.UsersService.PatientService;

namespace HealthcareBase.Service.UsersService.RegistrationService
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
                .SendActivationEmail(patientAccount.UserGuid,
                    patientAccount.Email,
                    emailTemplatePath);
        }

        public void ActivatePatient(Guid guid)
        {
            patientAccountService.ActivateAccount(guid);
        }

    }
}