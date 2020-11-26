// File:    MedicalConsumableRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface MedicalConsumableRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface IMedicalConsumableRepository : IWrappableRepository<MedicalConsumable, int>
    {
        bool ExistsByType(MedicalConsumableType type);
    }
}