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
            const int id = 1;
            const string email = "mail@mailprovider.com";
            const string emailTemplatePath = "/path/to/the/resource";

            var patientRegistrationService
                = new PatientRegistrationService(mockPatientAccountService.Object,mockNotifier.Object);
            patientRegistrationService.RegisterPatient(CreatePatientAccount(id,email),emailTemplatePath  );
            
            mockNotifier.Verify(n=>n.SendActivationEmail(id,"mail@mailprovider.com","/path/to/the/resource"));
        }

        private static PatientAccount CreatePatientAccount(int id, string email)
        {
            return new PatientAccount
            {
                Id=id,
                Email = email
            };
        }
    }
}