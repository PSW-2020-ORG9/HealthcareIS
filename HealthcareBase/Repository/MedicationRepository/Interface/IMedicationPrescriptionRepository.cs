// File:    MedicationPrescriptionRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:05:26
// Purpose: Definition of Interface MedicationPrescriptionRepository

using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.MedicationRepository.Interface
{
    public interface IMedicationPrescriptionRepository : IWrappableRepository<MedicationPrescription, int>
    {
    }
}