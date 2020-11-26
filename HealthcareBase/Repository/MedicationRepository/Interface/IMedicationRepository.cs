// File:    MedicationRepository.cs
// Author:  Win 10
// Created: 04 May 2020 12:04:39
// Purpose: Definition of Interface MedicationRepository

using HealthcareBase.Model.Medication;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.MedicationRepository.Interface
{
    public interface IMedicationRepository : IWrappableRepository<Medication, int>
    {
    }
}