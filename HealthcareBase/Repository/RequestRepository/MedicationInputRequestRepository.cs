// File:    MedicationInputRequestRepository.cs
// Author:  Korisnik
// Created: 29 May 2020 13:53:45
// Purpose: Definition of Interface MedicationInputRequestRepository

using Model.Requests;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.RequestRepository
{
    public interface MedicationInputRequestRepository : Repository<MedicationInputRequest, int>
    {
        IEnumerable<MedicationInputRequest> GetAllRejectedRequests();
        IEnumerable<MedicationInputRequest> GetAllPendingRequests(Model.Users.Employee.Doctor reviewer);
    }
}