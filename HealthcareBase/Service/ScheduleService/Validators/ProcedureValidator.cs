using System;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
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
        private readonly RepositoryWrapper<IRoomRepository> roomRepository;

        public ProcedureValidator(
            IDoctorRepository IDoctorRepository,
            IRoomRepository roomRepository,
            IPatientRepository IPatientRepository,
            IExaminationRepository examinationRepository)
        {
            this.doctorRepository = new RepositoryWrapper<IDoctorRepository>(IDoctorRepository);
            this.roomRepository = new RepositoryWrapper<IRoomRepository>(roomRepository);
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(IPatientRepository);
            this.examinationRepository = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
        }

        public void ValidateProcedure(Procedure procedure)
        {
            throw new NotImplementedException();
        }

        private void ValidateRequiredFields(Procedure procedure)
        {
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
            throw new NotImplementedException();
        }

        private void ValidateRoomTypeSuitability(ProcedureType procedureType, Room room)
        {
            if (procedureType.Kind == ProcedureKind.Examination && room.Purpose != RoomType.ExaminationRoom)
                throw new ValidationException();
            if (procedureType.Kind == ProcedureKind.Surgery && room.Purpose != RoomType.SurgeryRoom)
                throw new ValidationException();
        }

        private void ValidateEquipmentSuitability(ProcedureType procedureType, Room room)
        {
            throw new NotImplementedException();
        }
    }
}