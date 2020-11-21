// File:    MedicationPrescriptionFileRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Class MedicationPrescriptionFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace HealthcareBase.Repository.MedicationRepository
{
    public class MedicationPrescriptionFileRepository : GenericFileRepository<MedicationPrescription, int>,
        MedicationPrescriptionRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly MedicationRepository medicationRepository;
        private readonly PatientRepository patientRepository;

        public MedicationPrescriptionFileRepository(MedicationRepository medicationRepository,
            DoctorRepository doctorRepository,
            PatientRepository patientRepository, string filePath) : base(filePath)
        {
            this.medicationRepository = medicationRepository;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<MedicationPrescription> GetByPatient(Patient patient)
        {
            return GetMatching(medicationPrescription => medicationPrescription.Patient.Equals(patient));
        }

        protected override int GenerateKey(MedicationPrescription entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override MedicationPrescription ParseEntity(MedicationPrescription entity)
        {
            try
            {
                if (entity.Medication != null)
                    entity.Medication = medicationRepository.GetByID(entity.Medication.GetKey());
                if (entity.PrescribedBy != null)
                    entity.PrescribedBy = doctorRepository.GetByID(entity.PrescribedBy.GetKey());
                if (entity.Patient != null)
                    entity.Patient = patientRepository.GetByID(entity.Patient.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}