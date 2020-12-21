using System;
using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.ScheduleService.Validators
{
    public class HospitalizationValidator
    {
        private readonly RepositoryWrapper<IHospitalizationTypeRepository> hospitalizationTypeRepository;
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;

        public HospitalizationValidator(
            IRoomRepository roomRepository,
            IPatientRepository IPatientRepository,
            IHospitalizationTypeRepository IHospitalizationTypeRepository)
        {
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(IPatientRepository);
            this.hospitalizationTypeRepository =
                new RepositoryWrapper<IHospitalizationTypeRepository>(IHospitalizationTypeRepository);
        }

        public void ValidateHospitalization(Hospitalization hospitalization)
        {
            if (hospitalization is null)
                return;
            ValidateRequiredFields(hospitalization);
            ValidateAndUpdateReferences(hospitalization);
            ValidateRoomSuitability(hospitalization.Room);
        }

        private void ValidateRequiredFields(Hospitalization hospitalization)
        {
            if (hospitalization.HospitalizationType is null)
                throw new FieldRequiredException();
            if (hospitalization.Patient is null)
                throw new FieldRequiredException();
            if (hospitalization.Room is null)
                throw new FieldRequiredException();
            if (hospitalization.TimeInterval is null)
                throw new FieldRequiredException();
        }

        private void ValidateAndUpdateReferences(Hospitalization hospitalization)
        {
            try
            {
                hospitalization.Room = roomRepository.Repository.GetByID(hospitalization.Room.GetKey());
                hospitalization.Patient = patientRepository.Repository.GetByID(hospitalization.Patient.GetKey());
                hospitalization.HospitalizationType =
                    hospitalizationTypeRepository.Repository.GetByID(hospitalization.HospitalizationType.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }
        }

        private void ValidateRoomSuitability(Room room)
        {
            if (!room.Purpose.Equals(RoomType.RecoveryRoom))
                throw new ValidationException();
            if (room.Department is null)
                throw new ValidationException();
        }
    }
}