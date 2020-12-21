// File:    MedicationInputRequestRepository.cs
// Author:  Korisnik
// Created: 29 May 2020 13:53:45
// Purpose: Definition of Interface MedicationInputRequestRepository

using System.Collections.Generic;
using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.RequestRepository
{
    public interface IMedicationInputRequestRepository : IWrappableRepository<MedicationInputRequest, int>
    {
        IEnumerable<MedicationInputRequest> GetAllRejectedRequests();
        IEnumerable<MedicationInputRequest> GetAllPendingRequests(Doctor reviewer);
    }
}