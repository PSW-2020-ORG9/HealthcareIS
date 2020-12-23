using Moq;
using System.Collections.Generic;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.UsersService.PatientService;
using Xunit;

namespace HealthcareBaseUnitTests
{
    public class MedicalRecordTests
    {
        private Mock<IPatientRepository> _patientStubRepository;

        private void PrepareStubs()
        {
            _patientStubRepository = new Mock<IPatientRepository>();

            Patient p = new Patient();
            p.Id = 1;

            List<Patient> patients = new List<Patient>();
            patients.Add(p);

            _patientStubRepository.Setup(m => m.GetByID(1)).Returns(p);
            _patientStubRepository.Setup(m => m.GetAll()).Returns(patients);
        }

        [Fact]
        public void Finds_patient()
        {
            PrepareStubs();
            PatientService service = new PatientService
                (
                _patientStubRepository.Object
                );

            Patient result = service.GetByID(1);
            Assert.NotNull(result);
        }

        [Fact]
        public void Finds_no_patient()
        {
            PrepareStubs();
            PatientService service = new PatientService
                (
                _patientStubRepository.Object
                );
            Patient result = service.GetByID(2);
            Assert.Null(result);
        }
    }
}
