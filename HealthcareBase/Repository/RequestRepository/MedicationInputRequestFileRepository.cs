// File:    MedicationInputRequestFileRepository.cs
// Author:  Korisnik
// Created: 29 May 2020 13:54:21
// Purpose: Definition of Class MedicationInputRequestFileRepository

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;

namespace HealthcareBase.Repository.RequestRepository
{
    public class MedicationInputRequestFileRepository : GenericFileRepository<MedicationInputRequest, int>,
        MedicationInputRequestRepository
    {
        private readonly EmployeeAccountRepository employeeAccountRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly SpecialtyRepository specialtyRepository;

        public MedicationInputRequestFileRepository(EmployeeAccountRepository employeeAccountRepository,
            SpecialtyRepository specialtyRepository, string filePath) : base(filePath)
        {
            this.employeeAccountRepository = employeeAccountRepository;
            this.specialtyRepository = specialtyRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<MedicationInputRequest> GetAllRejectedRequests()
        {
            var inputRequests = GetAll();
            return GetMatching(input => input.Status.Equals(RequestStatus.Rejected));
        }

        public IEnumerable<MedicationInputRequest> GetAllPendingRequests(Doctor reviewer)
        {
            var inputRequests = GetAll();
            return GetMatching(input =>
                input.Status.Equals(RequestStatus.Pending) &&
                input.ReviewableBy.Intersect(reviewer.Specialties).Count() > 0);
        }

        protected override MedicationInputRequest ParseEntity(MedicationInputRequest entity)
        {
            try
            {
                if (entity.Reviewer != null)
                    entity.Reviewer = employeeAccountRepository.GetByID(entity.Reviewer.GetKey());
                if (entity.Sender != null)
                    entity.Sender = employeeAccountRepository.GetByID(entity.Sender.GetKey());
                var specialties = new List<Specialty>();
                foreach (var specialty in entity.ReviewableBy)
                    specialties.Add(specialtyRepository.GetByID(specialty.GetKey()));
                entity.ReviewableBy = specialties;
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }

        protected override int GenerateKey(MedicationInputRequest entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}