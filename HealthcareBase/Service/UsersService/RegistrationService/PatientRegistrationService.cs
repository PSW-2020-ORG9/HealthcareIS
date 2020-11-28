using System;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Service.UsersService.PatientService;

namespace HealthcareBase.Service.UsersService.RegistrationService
{
    public class PatientRegistrationService
    {
        private readonly IPatientAccountService patientAccountService;
        private readonly IRegistrationNotifier registrationNotifier;

        public PatientRegistrationService(IPatientAccountService patientAccountService)
        {
            this.patientAccountService = patientAccountService;
            registrationNotifier = new RegistrationNotifier();
        }
        public PatientRegistrationService(IPatientAccountService patientAccountService,IRegistrationNotifier registrationNotifier)
        {
            this.patientAccountService = patientAccountService;
            this.registrationNotifier = registrationNotifier;
        }

        public void RegisterPatient(PatientAccount patientAccount,string emailTemplatePath)
        {
            var createdPatientAccount = patientAccountService.CreateAccount(patientAccount);
            registrationNotifier
                .SendActivationEmail(patientAccount.Id,
                    patientAccount.Email,
                    emailTemplatePath);
        }

        public void ActivatePatient(int patientId)
        {
            patientAccountService.ActivateAccount(patientId);
        }

    }
}