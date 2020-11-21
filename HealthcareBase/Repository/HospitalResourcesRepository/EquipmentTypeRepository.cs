// File:    EquipmentTypeRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface EquipmentTypeRepository

using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface EquipmentTypeRepository : IWrappableRepository<EquipmentType, int>
    {
    }
}