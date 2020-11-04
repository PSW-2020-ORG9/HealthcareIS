using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.ScheduleService.Validators
{
    public class ProcedureValidator
    {
        private readonly RepositoryWrapper<DoctorRepository> doctorRepository;
        private readonly RepositoryWrapper<ExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<PatientRepository> patientRepository;
        private readonly RepositoryWrapper<ProcedureTypeRepository> procedureTypeRepository;
        private readonly RepositoryWrapper<RoomRepository> roomRepository;

        public ProcedureValidator(RepositoryWrapper<DoctorRepository> doctorRepository,
            RepositoryWrapper<RoomRepository> roomRepository,
            RepositoryWrapper<PatientRepository> patientRepository,
            RepositoryWrapper<ProcedureTypeRepository> procedureTypeRepository,
            RepositoryWrapper<ExaminationRepository> examinationRepository)
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
                procedure.Doctor = doctorRepository.Repository.GetByID(procedure.Doctor.GetKey());
                procedure.Room = roomRepository.Repository.GetByID(procedure.Room.GetKey());
                procedure.Patient = patientRepository.Repository.GetByID(procedure.Patient.GetKey());
                procedure.ProcedureType = procedureTypeRepository.Repository.GetByID(procedure.ProcedureType.GetKey());
                if (procedure.ReferredFrom != null)
                    procedure.ReferredFrom = examinationRepository.Repository.GetByID(procedure.ReferredFrom.GetKey());
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