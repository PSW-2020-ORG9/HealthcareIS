using System.Collections.Generic;
using Moq;
using User.API.Infrastructure.Repositories.Users.Patients.Interfaces;
using User.API.Model.Users.Patients;
using User.API.Model.Users.Patients.MedicalHistory;
using User.API.Model.Users.Patients.MedicalHistory.Relationship;
using User.API.Services.PatientService;
using Xunit;

namespace User.API.UnitTests
{
    public class PatientTests
    {
        Mock<IPatientRepository> _stubPatientRepository;

        [Fact]
        public void Patient_has_allergies()
        {
            PrepareStubs();
            var patientService = new PatientService(_stubPatientRepository.Object, null);

            var p = patientService.GetByID(1);

            Assert.NotNull(p.Allergies);
        }

        private void PrepareStubs()
        {
            _stubPatientRepository = new Mock<IPatientRepository>();
            _stubPatientRepository.Setup(m => m.GetByID(It.IsAny<int>())).Returns(CreatePatient());
        }

        private Patient CreatePatient()
        {
            var allergies = new List<AllergyManifestation>();
            allergies.Add(new AllergyManifestation { Intensity = AllergyIntensity.Mild });
            return new Patient
            {
                Id = 1,
                Allergies = allergies,
            };
        }
    }
}