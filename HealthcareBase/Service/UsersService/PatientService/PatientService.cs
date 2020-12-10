// File:    PatientService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientService

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.UsersService.PatientService
{
    public class PatientService
    {
        private readonly RepositoryWrapper<IExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<IHospitalizationRepository> hospitalizationRepository;
        private readonly RepositoryWrapper<IPatientRepository> patientRepository;
        private readonly RepositoryWrapper<ISurgeryRepository> surgeryRepository;

        public PatientService(
            IPatientRepository patientRepository,
            IExaminationRepository examinationRepository,
            ISurgeryRepository surgeryRepository,
            IHospitalizationRepository hospitalizationRepository)
        {
            this.patientRepository = new RepositoryWrapper<IPatientRepository>(patientRepository);
            this.examinationRepository = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this.surgeryRepository = new RepositoryWrapper<ISurgeryRepository>(surgeryRepository);
            this.hospitalizationRepository =
                new RepositoryWrapper<IHospitalizationRepository>(hospitalizationRepository);
        }

        public PatientChartDTO GetPatientChart(Patient patient)
        {
            var retPatientDTO = new PatientChartDTO();

            retPatientDTO.Examinations = examinationRepository.Repository.GetByPatientId(patient.Id);
            retPatientDTO.Surgeries = surgeryRepository.Repository.GetByPatient(patient);
            retPatientDTO.Hospitalizations = hospitalizationRepository.Repository.GetByPatient(patient);

            return retPatientDTO;
        }

        public Patient GetByID(int id)
        {
            return patientRepository.Repository.GetByID(id);
        }

        public IEnumerable<Patient> GetAllActive()
        {
            return patientRepository.Repository.GetMatching(patient => patient.Status == PatientStatus.Alive);
        }

        public Patient Create(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            return patientRepository.Repository.Create(patient);
        }

        public Patient Update(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            return patientRepository.Repository.Update(patient);
        }
    }
}