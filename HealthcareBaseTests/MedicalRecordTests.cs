using Model.Users.Generalities;
using Model.Users.Patient;
using Moq;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Service.UsersService.PatientService;
using System.Collections.Generic;
using Xunit;

namespace HealthcareBaseTests
{
    public class MedicalRecordTests
    {
        private Mock<PatientRepository> _patientStubRepository;
        private Mock<ExaminationRepository> _examinationStubRepository;
        private Mock<SurgeryRepository> _surgeryStubRepository;
        private Mock<HospitalizationRepository> _hospitalizationStubRepository;

        private void PrepareStubs()
        {
            _patientStubRepository = new Mock<PatientRepository>();
            _examinationStubRepository = new Mock<ExaminationRepository>();
            _surgeryStubRepository = new Mock<SurgeryRepository>();
            _hospitalizationStubRepository = new Mock<HospitalizationRepository>();

            Patient p = new Patient();
            p.MedicalRecordID = 1;

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
