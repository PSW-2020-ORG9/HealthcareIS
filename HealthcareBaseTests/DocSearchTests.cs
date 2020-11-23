using System.Collections.Generic;
using Model.Medication;
using Moq;
using Repository.MedicationRepository;

namespace HealthcareBaseTests
{
    public class DocSearchTests
    {
        private Mock<MedicationPrescriptionRepository> _prescriptionStubRepository;

        private void PrepareStubs()
        {
            _prescriptionStubRepository = new Mock<MedicationPrescriptionRepository>();

            var prescriptions = new List<MedicationPrescription>();
            var prescription1 = new MedicationPrescription()
            {
                
            };
        }
        
    }
}