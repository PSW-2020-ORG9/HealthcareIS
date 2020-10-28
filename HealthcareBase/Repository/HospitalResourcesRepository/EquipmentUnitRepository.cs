// File:    EquipmentUnitRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface EquipmentUnitRepository

using Model.HospitalResources;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.HospitalResourcesRepository
{
    public interface EquipmentUnitRepository : Repository<EquipmentUnit, int>
    {
        IEnumerable<EquipmentUnit> GetByCurrentLocationWithoutParse(Room room);
    }
}