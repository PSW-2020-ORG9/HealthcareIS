// File:    EquipmentTypeRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface EquipmentTypeRepository

using Model.HospitalResources;
using Repository.Generics;
using System;

namespace Repository.HospitalResourcesRepository
{
    public interface EquipmentTypeRepository : Repository<EquipmentType, int>
    {
    }
}