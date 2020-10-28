using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Repository.HospitalResourcesRepository;
using Repository.MiscellaneousRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ScheduleService.Validators
{
    public class HospitalizationValidator
    {
        private RoomRepository roomRepository;
        private EquipmentUnitRepository equipmentUnitRepository;
        private PatientRepository patientRepository;
        private HospitalizationTypeRepository hospitalizationTypeRepository;

        public HospitalizationValidator(RoomRepository roomRepository, EquipmentUnitRepository equipmentUnitRepository, 
            PatientRepository patientRepository, HospitalizationTypeRepository hospitalizationTypeRepository)
        {
            this.roomRepository = roomRepository;
            this.equipmentUnitRepository = equipmentUnitRepository;
            this.patientRepository = patientRepository;
            this.hospitalizationTypeRepository = hospitalizationTypeRepository;
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
                hospitalization.Room = roomRepository.GetByID(hospitalization.Room.GetKey());
                hospitalization.Patient = patientRepository.GetByID(hospitalization.Patient.GetKey());
                hospitalization.HospitalizationType = hospitalizationTypeRepository.GetByID(hospitalization.HospitalizationType.GetKey());
                List<EquipmentUnit> equipmentInUse = new List<EquipmentUnit>();
                foreach (EquipmentUnit equipment in hospitalization.EquipmentInUse)
                    equipmentInUse.Add(equipmentUnitRepository.GetByID(equipment.GetKey()));
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

        private void ValidateEquipmentSuitability(HospitalizationType hospitalizationType, IEnumerable<EquipmentUnit> equipment)
        {
            foreach (EquipmentType equipmentType in hospitalizationType.NecessaryEquipment)
            {
                int numberOfEquipmentUnitsOfCorrectType = equipment.Count(unit => unit.EquipmentType.Equals(equipmentType));
                if (numberOfEquipmentUnitsOfCorrectType == 0)
                    throw new ValidationException();
            }
        }

        private void ValidateEquipmentLocation(Room room, IEnumerable<EquipmentUnit> equipment)
        {
            foreach (EquipmentUnit equipmentUnit in equipment)
                if (!equipmentUnit.CurrentLocation.Equals(room))
                    throw new ValidationException();
        }
    }
}
