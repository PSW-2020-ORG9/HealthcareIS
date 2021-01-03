using System.Collections.Generic;
using System.Linq;
using Hospital.API.Infrastructure.Repositories.Medications;
using Hospital.API.Model.Medication;
using Hospital.API.Services.Medications;
using Moq;
using Xunit;

namespace Hospital.API.UnitTests
{
    public class DocSearchTests
    {
        private Mock<IMedicationPrescriptionRepository> _prescriptionStubRepository;

        // Prescriptions and past examinations 
        private void PrepareStubs()
        {
            _prescriptionStubRepository = new Mock<IMedicationPrescriptionRepository>();

            var allPrescriptions = new List<MedicationPrescription>();
            var matchedPrescriptions = new List<MedicationPrescription>();
            
            var prescription1 = new MedicationPrescription()
            {
                MedicalRecordId = 1,
                Medication = new Medication()
                {
                    Description = "Lek za smirenje",
                    Id = 1,
                    Name = "Bromazepam",
                    Manufacturer = "Pfizer"
                }
            };
            var prescription2 = new MedicationPrescription()
            {
                MedicalRecordId = 2,
                Medication = new Medication()
                {
                    Description = "Lek za bolove",
                    Id = 1,
                    Name = "Brufen",
                    Manufacturer = "Pfizer"
                }
            };
            
            allPrescriptions.Add(prescription1);
            allPrescriptions.Add(prescription2);
            matchedPrescriptions.Add(prescription2);

            _prescriptionStubRepository.Setup(repository =>
                repository.GetMatching(
                    prescription => prescription.Medication.Name.Contains("Brufen")))
                .Returns(matchedPrescriptions);
            
            _prescriptionStubRepository.Setup(repository =>
                    repository.GetMatching(
                        prescription => prescription.Medication.Name.Contains("Br")))
                .Returns(allPrescriptions);
        }

        [Fact]
        public void Find_specific_prescription()
        {
            PrepareStubs();
            var prescriptionService = new MedicationPrescriptionService
            (
                _prescriptionStubRepository.Object
            );

            IEnumerable<MedicationPrescription> matchedPrescriptions = prescriptionService.SimpleSearch("Brufen");
            Assert.Equal("Brufen", matchedPrescriptions.ToList()[0].Medication.Name);
        }
        
        [Fact]
        public void Find_no_prescription()
        {
            PrepareStubs();
            var prescriptionService = new MedicationPrescriptionService
            (
                _prescriptionStubRepository.Object
            );

            IEnumerable<MedicationPrescription> matchedPrescriptions = prescriptionService.SimpleSearch("xxx");
            Assert.Empty(matchedPrescriptions);
        }
        
        [Fact]
        public void Find_all_prescription()
        {
            PrepareStubs();
            var prescriptionService = new MedicationPrescriptionService
            (
                _prescriptionStubRepository.Object
            );

            IEnumerable<MedicationPrescription> matchedPrescriptions = prescriptionService.SimpleSearch("Br");
            Assert.All(matchedPrescriptions, prescription => 
                Assert.Contains("Br", prescription.Medication.Name));
        }
    }
}