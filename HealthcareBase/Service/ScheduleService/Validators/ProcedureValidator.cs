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
    }
}