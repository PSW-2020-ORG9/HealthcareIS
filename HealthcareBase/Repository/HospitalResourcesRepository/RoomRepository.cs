// File:    RoomRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RoomRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface RoomRepository : IWrappableRepository<Room, int>
    {
        IEnumerable<Room> GetByEquipment(IEnumerable<EquipmentType> equipment);

        IEnumerable<Room> GetByDepartment(Department department);
    }
}