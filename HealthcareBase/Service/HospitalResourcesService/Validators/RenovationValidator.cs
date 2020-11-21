﻿using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Service.HospitalResourcesService.Validators
{
    public class RenovationValidator
    {
        private readonly RepositoryWrapper<RoomRepository> roomRepository;

        public RenovationValidator(RoomRepository roomRepository)
        {
            this.roomRepository = new RepositoryWrapper<RoomRepository>(roomRepository);
        }

        public void ValidateRenovation(Renovation renovation)
        {
            if (renovation is null)
                return;
            ValidateRequiredFields(renovation);
            ValidateAndUpdateReferences(renovation);
        }

        private void ValidateRequiredFields(Renovation renovation)
        {
            if (renovation.Room is null)
                throw new FieldRequiredException();
            if (renovation.TimeInterval is null)
                throw new FieldRequiredException();
        }

        private void ValidateAndUpdateReferences(Renovation renovation)
        {
            try
            {
                renovation.Room = roomRepository.Repository.GetByID(renovation.Room.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }
        }
    }
}