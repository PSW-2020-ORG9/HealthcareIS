// File:    EquipmentUnitRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface EquipmentUnitRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface IEquipmentUnitRepository : IWrappableRepository<EquipmentUnit, int>
    {
    }
}