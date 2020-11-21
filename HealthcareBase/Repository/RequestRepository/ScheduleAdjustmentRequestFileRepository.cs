// File:    ScheduleAdjustmentRequestFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:30:24
// Purpose: Definition of Class ScheduleAdjustmentRequestFileRepository

using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.UserAccountsRepository;

namespace HealthcareBase.Repository.RequestRepository
{
    public class ScheduleAdjustmentRequestFileRepository : GenericFileRepository<ScheduleAdjustmentRequest, int>,
        ScheduleAdjustmentRequestRepository
    {
        private readonly EmployeeAccountRepository employeeAccountRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public ScheduleAdjustmentRequestFileRepository(EmployeeAccountRepository employeeAccountRepository,
            string filePath) : base(filePath)
        {
            this.employeeAccountRepository = employeeAccountRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(ScheduleAdjustmentRequest entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override ScheduleAdjustmentRequest ParseEntity(ScheduleAdjustmentRequest entity)
        {
            try
            {
                if (entity.Reviewer != null)
                    entity.Reviewer = employeeAccountRepository.GetByID(entity.Reviewer.GetKey());
                if (entity.Sender != null)
                    entity.Sender = employeeAccountRepository.GetByID(entity.Sender.GetKey());
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }
    }
}