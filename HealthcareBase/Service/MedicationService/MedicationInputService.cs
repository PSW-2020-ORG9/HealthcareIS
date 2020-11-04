// File:    MedicationInputService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationInputService

using System;
using System.Collections.Generic;
using Model.Requests;
using Model.StorageRecords;
using Model.Users.Employee;
using Repository.Generics;
using Repository.MedicationRepository;
using Repository.RequestRepository;

namespace Service.MedicationService
{
    public class MedicationInputService
    {
        private readonly RepositoryWrapper<MedicationInputRequestRepository> medicationInputRequestRepository;
        private readonly RepositoryWrapper<MedicationRepository> medicationRepository;
        private readonly RepositoryWrapper<MedicationStorageRepository> medicationStorageRepository;
        private readonly NotificationService.NotificationService notificationService;

        public MedicationInputService(
            RepositoryWrapper<MedicationInputRequestRepository> medicationInputRequestRepository,
            RepositoryWrapper<MedicationRepository> medicationRepository,
            NotificationService.NotificationService notificationService,
            RepositoryWrapper<MedicationStorageRepository> medicationStorageRepository
            )
        {
            this.medicationInputRequestRepository = medicationInputRequestRepository;
            this.medicationRepository = medicationRepository;
            this.notificationService = notificationService;
            this.medicationStorageRepository = medicationStorageRepository;
        }

        private void ChangeRequestFileds(MedicationInputRequestUpdateDTO requestUpdate, RequestStatus status)
        {
            var inputRequest = requestUpdate.InputRequest;
            inputRequest.Status = status;
            inputRequest.Reviewer = requestUpdate.Reviewer;
            inputRequest.ReviewerComment = requestUpdate.Comment;
            inputRequest.ReviewDate = DateTime.Now;
        }

        public MedicationInputRequest RequestInput(MedicationInputRequestDTO requestInput)
        {
            var inputRequest = new MedicationInputRequest
            {
                Medication = requestInput.Medication,
                ReviewableBy = requestInput.Specialties,
                Sender = requestInput.Sender,
                CreationDate = DateTime.Now
            };
            medicationInputRequestRepository.Repository.Create(inputRequest);
            return inputRequest;
        }

        public void ApproveInput(MedicationInputRequestUpdateDTO requestUpdate)
        {
            var inputRequest = requestUpdate.InputRequest;
            ChangeRequestFileds(requestUpdate, RequestStatus.Approved);
            var newMedication = inputRequest.Medication;
            medicationRepository.Repository.Create(newMedication);
            medicationStorageRepository.Repository.Create(new MedicationStorageRecord
                {Medication = newMedication, AvailableAmount = 0});
            medicationInputRequestRepository.Repository.Update(inputRequest);
        }

        public void DeclineInput(MedicationInputRequestUpdateDTO requestUpdate)
        {
            var inputRequest = requestUpdate.InputRequest;
            ChangeRequestFileds(requestUpdate, RequestStatus.Rejected);
            medicationInputRequestRepository.Repository.Update(inputRequest);
            notificationService.Notify(inputRequest);
        }

        public IEnumerable<MedicationInputRequest> GetAllRejectedRequests()
        {
            return medicationInputRequestRepository.Repository.GetAllRejectedRequests();
        }

        public IEnumerable<MedicationInputRequest> GetAllPendingRequests(Doctor reviewerSpecialty)
        {
            return medicationInputRequestRepository.Repository.GetAllPendingRequests(reviewerSpecialty);
        }
    }
}