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

            var patientRegistrationService
                = new PatientRegistrationService(mockPatientAccountService.Object,mockNotifier.Object);
            patientRegistrationService.RegisterPatient(CreatePatientAccount(guid,email),emailTemplatePath  );
            
            mockNotifier.Verify(n=>n.SendActivationEmail(guid,"mail@mailprovider.com","/path/to/the/resource"));
        }

        private static PatientAccount CreatePatientAccount(Guid guid, string email)
        {
            return new PatientAccount
            {
                Id = 1,
                UserGuid = guid,
                Email = email
            };
        }
    }
}