﻿using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.ScheduleService.Validators
{
    public class ProcedureValidator
    {
        private readonly DoctorRepository doctorRepository;
        private readonly ExaminationRepository examinationRepository;
        private readonly PatientRepository patientRepository;
        private readonly ProcedureTypeRepository procedureTypeRepository;
        private readonly RoomRepository roomRepository;

        public ProcedureValidator(DoctorRepository doctorRepository, RoomRepository roomRepository,
            PatientRepository patientRepository,
            ProcedureTypeRepository procedureTypeRepository, ExaminationRepository examinationRepository)
        {
            this.doctorRepository = doctorRepository;
            this.roomRepository = roomRepository;
            this.patientRepository = patientRepository;
            this.procedureTypeRepository = procedureTypeRepository;
            this.examinationRepository = examinationRepository;
        }

        public void ValidateProcedure(Procedure procedure)
        {
            if (procedure is null)
                return;
            ValidateRequiredFields(procedure);
            ValidateAndUpdateReferences(procedure);
            ValidateDoctorSuitability(procedure.ProcedureType, procedure.Doctor);
            ValidateRoomTypeSuitability(procedure.ProcedureType, procedure.Room);
            ValidateEquipmentSuitability(procedure.ProcedureType, procedure.Room);
        }

        private void ValidateRequiredFields(Procedure procedure)
        {
            if (procedure.ProcedureType is null)
                throw new FieldRequiredException();
            if (procedure.Doctor is null)
                throw new FieldRequiredException();
            if (procedure.Patient is null)
                throw new FieldRequiredException();
            if (procedure.Room is null)
                throw new FieldRequiredException();
            if (procedure.TimeInterval is null)
                throw new FieldRequiredException();
        }

        private void ValidateAndUpdateReferences(Procedure procedure)
        {
            try
            {
                procedure.Doctor = doctorRepository.GetByID(procedure.Doctor.GetKey());
                procedure.Room = roomRepository.GetByID(procedure.Room.GetKey());
                procedure.Patient = patientRepository.GetByID(procedure.Patient.GetKey());
                procedure.ProcedureType = procedureTypeRepository.GetByID(procedure.ProcedureType.GetKey());
                if (procedure.ReferredFrom != null)
                    procedure.ReferredFrom = examinationRepository.GetByID(procedure.ReferredFrom.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }
        }

        private void ValidateDoctorSuitability(ProcedureType procedureType, Doctor doctor)
        {
            var matchingSpeicalties = procedureType.QualifiedSpecialties.Intersect(doctor.Specialties);
            if (matchingSpeicalties.Count() == 0)
                throw new ValidationException();
        }

        private void ValidateRoomTypeSuitability(ProcedureType procedureType, Room room)
        {
            if (procedureType.Kind == ProcedureKind.Examination && room.Purpose != RoomType.examinationRoom)
                throw new ValidationException();
            if (procedureType.Kind == ProcedureKind.Surgery && room.Purpose != RoomType.operatingRoom)
                throw new ValidationException();
        }

        private void ValidateEquipmentSuitability(ProcedureType procedureType, Room room)
        {
            foreach (var type in procedureType.NecessaryEquipment)
            {
                var roomContainsEquipmentOfNecessaryType =
                    room.Equipment.Count(unit => unit.EquipmentType.Equals(type)) != 0;
                if (!roomContainsEquipmentOfNecessaryType)
                    throw new ValidationException();
            }
        }
    }
}