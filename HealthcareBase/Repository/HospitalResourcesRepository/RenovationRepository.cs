// File:    RenovationRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RenovationRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface RenovationRepository : IWrappableRepository<Renovation, int>
    {
        IEnumerable<Renovation> getByRoomAndTime(Room room, TimeInterval time);
    }
}