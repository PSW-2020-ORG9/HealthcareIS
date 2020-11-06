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

        public ProcedureValidator(
            DoctorRepository doctorRepository,
            RoomRepository roomRepository,
            PatientRepository patientRepository,
            ProcedureTypeRepository procedureTypeRepository,
            ExaminationRepository examinationRepository)
        {
            this.doctorRepository = new RepositoryWrapper<DoctorRepository>(doctorRepository);
            this.roomRepository = new RepositoryWrapper<RoomRepository>(roomRepository);
            this.patientRepository = new RepositoryWrapper<PatientRepository>(patientRepository);
            this.procedureTypeRepository = new RepositoryWrapper<ProcedureTypeRepository>(procedureTypeRepository);
            this.examinationRepository = new RepositoryWrapper<ExaminationRepository>(examinationRepository);
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
            var matchingSpecialties = procedureType.QualifiedSpecialties.Intersect(doctor.Specialties);
            if (!matchingSpecialties.Any())
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
            if (procedureType.NecessaryEquipment
                .Select(type => room.Equipment.Count(unit => unit.EquipmentType.Equals(type)) != 0)
                .Any(roomContainsEquipmentOfNecessaryType => !roomContainsEquipmentOfNecessaryType))
            {
                throw new ValidationException();
            }
        }
    }
}