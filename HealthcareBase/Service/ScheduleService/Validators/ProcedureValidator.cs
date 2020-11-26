using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.ScheduleService.Validators
{
    public class ProcedureValidator
    {
        private readonly RepositoryWrapper<IDoctorRepository> doctorRepository;
        private readonly RepositoryWrapper<IExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;
        private readonly RepositoryWrapper<IProcedureTypeRepository> procedureTypeRepository;
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;

        public ProcedureValidator(
            IDoctorRepository IDoctorRepository,
            IRoomRepository roomRepository,
            IPatientRepository IPatientRepository,
            IProcedureTypeRepository procedureTypeRepository,
            IExaminationRepository examinationRepository)
        {
            this.doctorRepository = new RepositoryWrapper<IDoctorRepository>(IDoctorRepository);
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(IPatientRepository);
            this.procedureTypeRepository = new RepositoryWrapper<IProcedureTypeRepository>(procedureTypeRepository);
            this.examinationRepository = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
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