using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.UsersService.PatientService;
using Moq;
using Xunit;

namespace HealthcareBaseTests
{
    public class PatientTests
    {
        Mock<IPatientRepository> _stubPatientRepository;
        [Fact]
        public void Patient_has_examinations()
        {
            PrepareStubs();
            var patientService = new PatientService(_stubPatientRepository.Object, null, null, null);

            Patient p = patientService.GetByID(1);

            Assert.NotNull(p.Examinations);
        }

        [Fact]
        public void Patient_has_allergies()
        {
            PrepareStubs();
            var patientService = new PatientService(_stubPatientRepository.Object, null, null, null);

            Patient p = patientService.GetByID(1);

            Assert.NotNull(p.Allergies);
        }

        private void PrepareStubs()
        {
            _stubPatientRepository = new Mock<IPatientRepository>();
            _stubPatientRepository.Setup(m => m.GetByID(It.IsAny<int>())).Returns(CreatePatient());
        }

        private Patient CreatePatient()
        {
            List<AllergyManifestation> allergies = new List<AllergyManifestation>();
            allergies.Add(new AllergyManifestation { Intensity = AllergyIntensity.Mild });
            List<Examination> examinations = new List<Examination>();
            examinations.Add(new Examination { Id = 1 });
            return new Patient
            {
                Id = 1,
                Allergies = allergies,
                Examinations = examinations
            };
        }
    }
}