// File:    ScheduleAdjustmentRequestFileRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:30:24
// Purpose: Definition of Class ScheduleAdjustmentRequestFileRepository

using Model.CustomExceptions;
using Model.Requests;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.UserAccountsRepository;
using System;

namespace Repository.RequestRepository
{
    public class ScheduleAdjustmentRequestFileRepository : GenericFileRepository<ScheduleAdjustmentRequest, int>, ScheduleAdjustmentRequestRepository
    {
        private IntegerKeyGenerator keyGenerator;
        EmployeeAccountRepository employeeAccountRepository;

        public ScheduleAdjustmentRequestFileRepository(EmployeeAccountRepository employeeAccountRepository, String filePath) : base(filePath)
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