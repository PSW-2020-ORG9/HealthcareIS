using System;
using HealthcareBase.Model.Users.UserAccounts;
using HealthcareBase.Service.UsersService.PatientService;
using HealthcareBase.Service.UsersService.RegistrationService;
using Moq;
using Xunit;

namespace HealthcareBaseTests
{
    public class PatientRegistrationTests
    {
        [Fact]
        public void Sends_notification_when_creating_patient()
        {
            var mockNotifier = new Mock<IRegistrationNotifier>();
            var mockPatientAccountService = new Mock<IPatientAccountService>();
            var guid = new Guid();
            const string email = "mail@mailprovider.com";
            const string emailTemplatePath = "/path/to/the/resource";

            var patientRegistrationService
                = new PatientRegistrationService(mockPatientAccountService.Object,mockNotifier.Object);
            patientRegistrationService.RegisterPatient(CreatePatientAccount(guid,email),emailTemplatePath  );
            
            mockNotifier.Verify(n=>n.SendActivationEmail(guid,"mail@mailprovider.com","/path/to/the/resource"));
        }

        private static PatientAccount CreatePatientAccount(Guid userGuid, string email)
        {
            return new PatientAccount
            {
                UserGuid = userGuid,
                Email = email
            };
        }
    }
}