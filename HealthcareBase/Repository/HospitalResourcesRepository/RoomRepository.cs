// File:    RoomRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RoomRepository

using System.Collections.Generic;
using Model.HospitalResources;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface RoomRepository : Repository<Room, int>
    {
        IEnumerable<Room> GetByEquipment(IEnumerable<EquipmentType> equipment);

        IEnumerable<Room> GetByDepartment(Department department);
    }
}