using System;
using Moq;
using User.API.Model.Users.UserAccounts;
using User.API.Services.PatientService;
using User.API.Services.RegistrationService;
using Xunit;

namespace User.API.UnitTests
{
    public class PatientRegistrationTests
    {
        [Fact]
        public void Sends_notification_when_creating_patient()
        {
            var mockNotifier = new Mock<IRegistrationNotifier>();
            var mockPatientAccountService = new Mock<IPatientAccountService>();
            var guid = Guid.NewGuid();
            const string email = "mail@mailprovider.com";
            const string emailTemplatePath = "/path/to/the/resource";
            var patientAccount = CreatePatientAccount(guid, email);

            mockPatientAccountService.Setup(pa => pa.CreateAccount(patientAccount)).Returns(patientAccount);
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