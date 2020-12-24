using System;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using Moq;
using Xunit;

namespace HealthcareBaseUnitTests
{
    public class PatientRegistrationTests
    {
        [Fact]
        public void Sends_notification_when_creating_patient()
        {
            var mockNotifier = new Mock<IRegistrationNotifier>();
            var mockPatientAccountService = new Mock<IPatientAccountService>();
            Guid guid = Guid.NewGuid();
            const string email = "mail@mailprovider.com";
            const string emailTemplatePath = "/path/to/the/resource";
            var patientAccount = CreatePatientAccount(guid, email);

            var patientRegistrationService
                = new PatientRegistrationService(mockPatientAccountService.Object,mockNotifier.Object);
            patientRegistrationService.RegisterPatient(patientAccount,emailTemplatePath);
            
            mockNotifier.Verify(n=>
                n.SendActivationEmail(patientAccount,"/path/to/the/resource"));
        }

        private static PatientAccount CreatePatientAccount(Guid guid, string email)
        {
            return new PatientAccount
            {
                Id = 1,
                UserGuid = guid,
                Credentials = new Credentials
                {
                    Email = email
                }
            };
        }
    }
}