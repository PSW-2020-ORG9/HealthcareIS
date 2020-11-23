using System;
using System.Collections.Generic;
using Model.Medication;
using Model.Miscellaneous;
using Moq;
using Repository.MedicationRepository;

namespace HealthcareBaseTests
{
    public class DocSearchTests
    {
        private Mock<MedicationPrescriptionRepository> _prescriptionStubRepository;

        // Prescriptions and past examinations 
        private void PrepareStubs()
        {
            _prescriptionStubRepository = new Mock<MedicationPrescriptionRepository>();

            var prescription1 = new MedicationPrescription()
            {
                MedicalRecordId = 1,
                Medication = new Medication()
                {
                    Description = "Neki lekic za naseg pacijenta",
                    Id = 1,
                    Name = "Bromazopol",
                    Manufacturer = "Pfizer"
                },
                Diagnosis = new Diagnosis()
                {
                    Icd = "AVC",
                    Name = "Vrtoglavica",
                }
            };
            var prescription2 = new MedicationPrescription()
            {
                MedicalRecordId = 2,
                Medication = new Medication()
                {
                    Description = "Lek za pacijenta broj 2",
                    Id = 1,
                    Name = "Hidrogenizovani rastvor",
                    Manufacturer = "Pfizer"
                },
                Diagnosis = new Diagnosis()
                {
                    Icd = "RRT4",
                    Name = "Umor i pospanost",
                }
            };
        }
        
    }
}