// File:    MedicationInputRequestFileRepository.cs
// Author:  Korisnik
// Created: 29 May 2020 13:54:21
// Purpose: Definition of Class MedicationInputRequestFileRepository

using Model.CustomExceptions;
using Model.Requests;
using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using Repository.MedicationRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Repository.UsersRepository.UserAccountsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RequestRepository
{
    public class MedicationInputRequestFileRepository : GenericFileRepository<MedicationInputRequest, int>, MedicationInputRequestRepository
    {
        private IntegerKeyGenerator keyGenerator;
        EmployeeAccountRepository employeeAccountRepository;
        SpecialtyRepository specialtyRepository;

        public MedicationInputRequestFileRepository(EmployeeAccountRepository employeeAccountRepository,
            SpecialtyRepository specialtyRepository, String filePath) : base(filePath)
        {
            this.employeeAccountRepository = employeeAccountRepository;
            this.specialtyRepository = specialtyRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override MedicationInputRequest ParseEntity(MedicationInputRequest entity)
        {
            try
            {
                if (entity.Reviewer != null)
                    entity.Reviewer = employeeAccountRepository.GetByID(entity.Reviewer.GetKey());
                if (entity.Sender != null)
                    entity.Sender = employeeAccountRepository.GetByID(entity.Sender.GetKey());
                List<Specialty> specialties = new List<Specialty>();
                foreach (Specialty specialty in entity.ReviewableBy)
                    specialties.Add(specialtyRepository.GetByID(specialty.GetKey()));
                entity.ReviewableBy = specialties;
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }

        public IEnumerable<MedicationInputRequest> GetAllRejectedRequests()
        {
            IEnumerable<MedicationInputRequest> inputRequests = GetAll();
            return GetMatching(input => input.Status.Equals(RequestStatus.Rejected));
        }

        public IEnumerable<MedicationInputRequest> GetAllPendingRequests(Model.Users.Employee.Doctor reviewer)
        {
            IEnumerable<MedicationInputRequest> inputRequests = GetAll();
            return GetMatching(input => input.Status.Equals(RequestStatus.Pending) && input.ReviewableBy.Intersect(reviewer.Specialties).Count() > 0);
        }

        protected override int GenerateKey(MedicationInputRequest entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}