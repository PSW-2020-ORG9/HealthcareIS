// File:    RenovationRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RenovationRepository

using System.Collections.Generic;
using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface RenovationRepository : IWrappableRepository<Renovation, int>
    {
        IEnumerable<Renovation> getByRoomAndTime(Room room, TimeInterval time);
    }
}