// File:    MedicationInputRequestRepository.cs
// Author:  Korisnik
// Created: 29 May 2020 13:53:45
// Purpose: Definition of Interface MedicationInputRequestRepository

using System.Collections.Generic;
using Model.Requests;
using Model.Users.Employee;
using Repository.Generics;

namespace Repository.RequestRepository
{
    public interface MedicationInputRequestRepository : IWrappableRepository<MedicationInputRequest, int>
    {
        IEnumerable<MedicationInputRequest> GetAllRejectedRequests();
        IEnumerable<MedicationInputRequest> GetAllPendingRequests(Doctor reviewer);
    }
}