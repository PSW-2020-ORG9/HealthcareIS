using System.Collections.Generic;
using System.Linq;
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
        private readonly RepositoryWrapper<IEquipmentUnitRepository> equipmentUnitRepository;
        private readonly RepositoryWrapper<IHospitalizationTypeRepository> hospitalizationTypeRepository;
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;

        public HospitalizationValidator(
            IRoomRepository roomRepository,
            IEquipmentUnitRepository equipmentUnitRepository,
            IPatientRepository IPatientRepository,
            IHospitalizationTypeRepository IHospitalizationTypeRepository)
        {
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
            this.equipmentUnitRepository = new RepositoryWrapper<IEquipmentUnitRepository>(equipmentUnitRepository);
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
            ValidateRoomSuitability(hospitalization.HospitalizationType, hospitalization.Room);
            ValidateEquipmentSuitability(hospitalization.HospitalizationType, hospitalization.EquipmentInUse);
            ValidateEquipmentLocation(hospitalization.Room, hospitalization.EquipmentInUse);
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
                var equipmentInUse = new List<EquipmentUnit>();
                foreach (var equipment in hospitalization.EquipmentInUse)
                    equipmentInUse.Add(equipmentUnitRepository.Repository.GetByID(equipment.GetKey()));
                hospitalization.EquipmentInUse = equipmentInUse;
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }
        }

        private void ValidateRoomSuitability(HospitalizationType hospitalizationType, Room room)
        {
            if (!room.Purpose.Equals(RoomType.recoveryRoom))
                throw new ValidationException();
            if (room.Department is null)
                throw new ValidationException();
            if (!hospitalizationType.AppropriateDepartments.Contains(room.Department))
                throw new ValidationException();
        }

        private void ValidateEquipmentSuitability(HospitalizationType hospitalizationType,
            IEnumerable<EquipmentUnit> equipment)
        {
            foreach (var equipmentType in hospitalizationType.NecessaryEquipment)
            {
                var numberOfEquipmentUnitsOfCorrectType =
                    equipment.Count(unit => unit.EquipmentType.Equals(equipmentType));
                if (numberOfEquipmentUnitsOfCorrectType == 0)
                    throw new ValidationException();
            }
        }

        private void ValidateEquipmentLocation(Room room, IEnumerable<EquipmentUnit> equipment)
        {
            foreach (var equipmentUnit in equipment)
                if (!equipmentUnit.CurrentLocation.Equals(room))
                    throw new ValidationException();
        }
    }
}