// File:    RenovationService.cs
// Author:  Lana
// Created: 28 May 2020 11:49:19
// Purpose: Definition of Class RenovationService

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Utilities;
using Repository.HospitalResourcesRepository;
using Service.HospitalResourcesService.Validators;
using System;
using System.Collections.Generic;

namespace Service.HospitalResourcesService.RoomService
{
    public class RenovationService
    {
        private RenovationRepository renovationRepository;
        private RenovationValidator renovationValidator;
        private RenovationScheduleComplianceValidator scheduleValidator;
        private TimeSpan timeLimit;

        public RenovationScheduleComplianceValidator ScheduleValidator { get => scheduleValidator; set => scheduleValidator = value; }

        public RenovationService(RenovationRepository renovationRepository, RenovationValidator renovationValidator, TimeSpan timeLimit)
        {
            this.renovationRepository = renovationRepository;
            this.renovationValidator = renovationValidator;
            this.timeLimit = timeLimit;
        }

        public Renovation GetByID(int id)
        {
            return renovationRepository.GetByID(id);
        }

        public IEnumerable<Renovation> GetAll()
        {
            return renovationRepository.GetAll();
        }

        public IEnumerable<Renovation> GetByRoomAndTime(Room room, TimeInterval time)
        {
            return renovationRepository.getByRoomAndTime(room, time);
        }

        public Renovation Schedule(Renovation renovation)
        {
            if (renovation is null)
                throw new BadRequestException();
            ValidateForScheduling(renovation);
            return renovationRepository.Create(renovation);
        }

        public Renovation Reschedule(Renovation renovation)
        {
            if (renovation is null)
                throw new BadRequestException();
            ValidateForRescheduling(renovation);
            return renovationRepository.Update(renovation);
        }

        public void Cancel(Renovation renovation)
        {
            if (renovation is null)
                throw new BadRequestException();
            ValidateForCancelling(renovation);
            renovationRepository.Delete(renovation);
        }

        private void ValidateForScheduling(Renovation renovation)
        {
            ValidateStartTimeLimit(renovation); 
            renovationValidator.ValidateRenovation(renovation); 
            scheduleValidator.ValidateComplianceForScheduling(renovation);   
            ValidateStartTimeLimit(renovation);
        }

        private void ValidateForRescheduling(Renovation renovation)
        {
            Renovation oldRenovation = renovationRepository.GetByID(renovation.GetKey());
            renovationValidator.ValidateRenovation(renovation);
            if (!oldRenovation.Room.Equals(renovation.Room))
                throw new BadRequestException();
            if (renovation.TimeInterval.Start.Equals(oldRenovation.TimeInterval.Start))
                ValidateForEndRescheduling(renovation, oldRenovation);
            else
                ValidateForStartRescheduling(renovation, oldRenovation);
        }

        private void ValidateForStartRescheduling(Renovation newRenovation, Renovation oldRenovation)
        {
            ValidateStartTimeLimit(newRenovation);
            ValidateStartTimeLimit(oldRenovation);
            scheduleValidator.ValidateComplianceForRescheduling(newRenovation);
            ValidateStartTimeLimit(newRenovation);
            ValidateStartTimeLimit(oldRenovation);
        }

        private void ValidateForEndRescheduling(Renovation newRenovation, Renovation oldRenovation)
        {
            ValidateEndTimeLimit(oldRenovation);
            ValidateEndTimeLimit(newRenovation);
            scheduleValidator.ValidateComplianceForRescheduling(newRenovation);
            ValidateEndTimeLimit(oldRenovation);
            ValidateEndTimeLimit(newRenovation);
        }

        private void ValidateForCancelling(Renovation renovation)
        {
            ValidateStartTimeLimit(renovation);
        }

        private void ValidateStartTimeLimit(Renovation renovation)
        {
            if (renovation.TimeInterval.Start <= DateTime.Now.Date + timeLimit)
                throw new TimingException();
        }

        private void ValidateEndTimeLimit(Renovation renovation)
        {
            if (renovation.TimeInterval.End < DateTime.Now.Date)
                throw new TimingException();
        }

    }
}