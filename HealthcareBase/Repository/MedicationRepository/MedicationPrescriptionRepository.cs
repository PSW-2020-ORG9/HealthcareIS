// File:    MedicationPrescriptionRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Interface MedicationPrescriptionRepository

using System.Collections.Generic;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.MedicationRepository
{
    public interface MedicationPrescriptionRepository : IWrappableRepository<MedicationPrescription, int>
    {
        IEnumerable<MedicationPrescription> GetByPatient(Patient patient);
    }
}