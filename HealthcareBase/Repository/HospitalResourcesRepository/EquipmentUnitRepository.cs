// File:    EquipmentUnitRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface EquipmentUnitRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface EquipmentUnitRepository : IWrappableRepository<EquipmentUnit, int>
    {
        IEnumerable<EquipmentUnit> GetByCurrentLocationWithoutParse(Room room);
    }
}