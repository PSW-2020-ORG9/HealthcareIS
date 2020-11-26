using Moq;
using System.Collections.Generic;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.UsersService.PatientService;
using Xunit;

namespace HealthcareBaseTests
{
    public class MedicalRecordTests
    {
        private Mock<IPatientRepository> _patientStubRepository;
        private Mock<IExaminationRepository> _examinationStubRepository;
        private Mock<ISurgeryRepository> _surgeryStubRepository;
        private Mock<IHospitalizationRepository> _hospitalizationStubRepository;

        private void PrepareStubs()
        {
            _patientStubRepository = new Mock<IPatientRepository>();
            _examinationStubRepository = new Mock<IExaminationRepository>();
            _surgeryStubRepository = new Mock<ISurgeryRepository>();
            _hospitalizationStubRepository = new Mock<IHospitalizationRepository>();

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
                _patientStubRepository.Object,
                _examinationStubRepository.Object,
                _surgeryStubRepository.Object,
                _hospitalizationStubRepository.Object
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
                _patientStubRepository.Object,
                _examinationStubRepository.Object,
                _surgeryStubRepository.Object,
                _hospitalizationStubRepository.Object
                );
            Patient result = service.GetByID(2);
            Assert.Null(result);
        }
    }
}
