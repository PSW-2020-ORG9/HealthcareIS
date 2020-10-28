// File:    MedicationInputService.cs
// Author:  Lana
// Created: 27 May 2020 19:21:11
// Purpose: Definition of Class MedicationInputService

using Model.Medication;
using Model.Requests;
using Model.Users.Employee;
using Repository.MedicationRepository;
using Repository.RequestRepository;
using System;
using System.Collections.Generic;
using Model.StorageRecords;

namespace Service.MedicationService
{
    public class MedicationInputService
    {
        private MedicationInputRequestRepository medicationInputRequestRepository;
        private MedicationRepository medicationRepository;
        private NotificationService.NotificationService notificationService;
        private MedicationStorageRepository medicationStorageRepository;

        public MedicationInputService(MedicationInputRequestRepository medicationInputRequestRepository, 
            MedicationRepository medicationRepository, NotificationService.NotificationService notificationService, 
            MedicationStorageRepository medicationStorageRepository)
        {
            this.medicationInputRequestRepository = medicationInputRequestRepository;
            this.medicationRepository = medicationRepository;
            this.notificationService = notificationService;
            this.medicationStorageRepository = medicationStorageRepository;
        }

        private void ChangeRequestFileds(MedicationInputRequestUpdateDTO requestUpdate, RequestStatus status)
        {
            MedicationInputRequest inputRequest = requestUpdate.InputRequest;
            inputRequest.Status = status;
            inputRequest.Reviewer = requestUpdate.Reviewer;
            inputRequest.ReviewerComment = requestUpdate.Comment;
            inputRequest.ReviewDate = DateTime.Now;
        }

        public MedicationInputRequest RequestInput(MedicationInputRequestDTO requestInput)
        {
            MedicationInputRequest inputRequest = new MedicationInputRequest()
            {
                Medication = requestInput.Medication,
                ReviewableBy = requestInput.Specialties,
                Sender = requestInput.Sender,
                CreationDate = DateTime.Now
            };
            medicationInputRequestRepository.Create(inputRequest);
            return inputRequest;
        }

        public void ApproveInput(MedicationInputRequestUpdateDTO requestUpdate)
        {
            MedicationInputRequest inputRequest = requestUpdate.InputRequest;
            ChangeRequestFileds(requestUpdate, RequestStatus.Approved);
            Medication newMedication = inputRequest.Medication;
            medicationRepository.Create(newMedication);
            medicationStorageRepository.Create(new MedicationStorageRecord() { Medication = newMedication, AvailableAmount = 0 });
            medicationInputRequestRepository.Update(inputRequest);

        }

        public void DeclineInput(MedicationInputRequestUpdateDTO requestUpdate)
        {
            MedicationInputRequest inputRequest = requestUpdate.InputRequest;
            ChangeRequestFileds(requestUpdate, RequestStatus.Rejected);
            medicationInputRequestRepository.Update(inputRequest);
            notificationService.Notify(inputRequest);
        }

        public IEnumerable<MedicationInputRequest> GetAllRejectedRequests()
        {
            return medicationInputRequestRepository.GetAllRejectedRequests();
        }

        public IEnumerable<MedicationInputRequest> GetAllPendingRequests(Doctor reviewerSpecialty)
        {
            return medicationInputRequestRepository.GetAllPendingRequests(reviewerSpecialty);
        }

    }
}