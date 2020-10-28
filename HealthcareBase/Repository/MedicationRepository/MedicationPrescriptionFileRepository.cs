// File:    MedicationPrescriptionFileRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Class MedicationPrescriptionFileRepository

using Model.Medication;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;
using Model.Users.Patient;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Model.CustomExceptions;

namespace Repository.MedicationRepository
{
    public class MedicationPrescriptionFileRepository : GenericFileRepository<MedicationPrescription, int>, MedicationPrescriptionRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private MedicationRepository medicationRepository;
        private DoctorRepository doctorRepository;
        private PatientRepository patientRepository;

        public MedicationPrescriptionFileRepository(MedicationRepository medicationRepository, DoctorRepository doctorRepository,
            PatientRepository patientRepository, String filePath) : base(filePath)
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