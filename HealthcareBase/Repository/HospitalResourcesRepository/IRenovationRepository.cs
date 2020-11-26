// File:    RenovationRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RenovationRepository

using System.Collections.Generic;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.HospitalResourcesRepository
{
    public interface IRenovationRepository : IWrappableRepository<Renovation, int>
    {
        IEnumerable<Renovation> getByRoomAndTime(Room room, TimeInterval time);
    }
}