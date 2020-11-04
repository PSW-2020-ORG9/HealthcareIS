// File:    ScheduleAdjustmentRequestService.cs
// Author:  Lana
// Created: 02 June 2020 01:36:41
// Purpose: Definition of Class ScheduleAdjustmentRequestService

using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Requests;
using Model.Users.UserAccounts;
using Repository.Generics;
using Repository.RequestRepository;
using Repository.UsersRepository.UserAccountsRepository;

namespace Service.ScheduleService.ScheduleAdjustmentRequestService
{
    public class ScheduleAdjustmentRequestService
    {
        private readonly RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository;
        private readonly NotificationService.NotificationService notificationService;
        private readonly RepositoryWrapper<ScheduleAdjustmentRequestRepository> requestRepository;

        public ScheduleAdjustmentRequestService(RepositoryWrapper<ScheduleAdjustmentRequestRepository> requestRepository,
            RepositoryWrapper<EmployeeAccountRepository> employeeAccountRepository,
            NotificationService.NotificationService notificationService)
        {
            this.requestRepository = requestRepository;
            this.employeeAccountRepository = employeeAccountRepository;
            this.notificationService = notificationService;
        }

        public IEnumerable<ScheduleAdjustmentRequest> GetAllPending()
        {
            return requestRepository.Repository.GetMatching(
                request => request.Status.Equals(RequestStatus.Pending));
        }

        public void Accept(RequestStatusUpdateDTO update)
        {
            Update(update, RequestStatus.Approved);
        }

        public void Decline(RequestStatusUpdateDTO update)
        {
            Update(update, RequestStatus.Rejected);
        }

        private void Update(RequestStatusUpdateDTO update, RequestStatus status)
        {
            if (update is null || update.Reviewer is null || update.Request is null)
                throw new BadRequestException();
            var request = requestRepository.Repository.GetByID(update.Request.GetKey());
            var reviewer = employeeAccountRepository.Repository.GetByID(update.Reviewer.GetKey());
            if (reviewer.EmployeeType != EmployeeType.Secretary)
                throw new ValidationException();

            request.Reviewer = reviewer;
            request.ReviewerComment = update.Comment;
            request.ReviewDate = DateTime.Now;
            request.Status = status;

            var updated = requestRepository.Repository.Update(request);
            notificationService.Notify(updated);
        }

        public ScheduleAdjustmentRequest Create(ScheduleAdjustmentRequest request)
        {
            if (request is null || request.Sender is null)
                throw new BadRequestException();
            if (!employeeAccountRepository.Repository.ExistsByID(request.Sender.GetKey()))
                throw new BadReferenceException();

            request.Status = RequestStatus.Pending;
            request.CreationDate = DateTime.Now;

            return requestRepository.Repository.Create(request);
        }
    }
}