// File:    RenovationRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface RenovationRepository

using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.HospitalResourcesRepository
{
   public interface RenovationRepository : Repository<Renovation,int>
   {
        IEnumerable<Renovation> getByRoomAndTime(Room room, TimeInterval time);
   }
}