// File:    ScheduleAdjustmentRequestService.cs
// Author:  Lana
// Created: 02 June 2020 01:36:41
// Purpose: Definition of Class ScheduleAdjustmentRequestService

using Model.CustomExceptions;
using Model.Requests;
using Model.Users.UserAccounts;
using Repository.RequestRepository;
using Repository.UsersRepository.UserAccountsRepository;
using System;
using System.Collections.Generic;

namespace Service.ScheduleService.ScheduleAdjustmentRequestService
{
    public class ScheduleAdjustmentRequestService
    {
        private ScheduleAdjustmentRequestRepository requestRepository;
        private EmployeeAccountRepository employeeAccountRepository;
        private NotificationService.NotificationService notificationService;

        public ScheduleAdjustmentRequestService(ScheduleAdjustmentRequestRepository requestRepository, 
            EmployeeAccountRepository employeeAccountRepository, NotificationService.NotificationService notificationService)
        {
            this.requestRepository = requestRepository;
            this.employeeAccountRepository = employeeAccountRepository;
            this.notificationService = notificationService;
        }

        public IEnumerable<ScheduleAdjustmentRequest> GetAllPending()
        {
            return requestRepository.GetMatching(
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
            ScheduleAdjustmentRequest request = requestRepository.GetByID(update.Request.GetKey());
            EmployeeAccount reviewer = employeeAccountRepository.GetByID(update.Reviewer.GetKey());
            if (reviewer.EmployeeType != EmployeeType.Secretary)
                throw new ValidationException();

            request.Reviewer = reviewer;
            request.ReviewerComment = update.Comment;
            request.ReviewDate = DateTime.Now;
            request.Status = status;

            ScheduleAdjustmentRequest updated = requestRepository.Update(request);
            notificationService.Notify(updated);
        }

        public ScheduleAdjustmentRequest Create(ScheduleAdjustmentRequest request)
        {
            if (request is null || request.Sender is null)
                throw new BadRequestException();
            if (!employeeAccountRepository.ExistsByID(request.Sender.GetKey()))
                throw new BadReferenceException();

            request.Status = RequestStatus.Pending;
            request.CreationDate = DateTime.Now;

            return requestRepository.Create(request);
        }

    }
}