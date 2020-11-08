// File:    PatientService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientService

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Users.Patient;
using Repository.Generics;
using Repository.ScheduleRepository.HospitalizationsRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.UsersService.PatientService
{
    public class PatientService
    {
        private readonly RepositoryWrapper<ExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<HospitalizationRepository> hospitalizationRepository;
        private readonly PatientAccountService patientAccountService;
        private readonly RepositoryWrapper<PatientRepository> patientRepository;
        private readonly RepositoryWrapper<SurgeryRepository> surgeryRepository;

        public PatientService(
            PatientRepository patientRepository,
            ExaminationRepository examinationRepository,
            SurgeryRepository surgeryRepository,
            HospitalizationRepository hospitalizationRepository,
            PatientAccountService patientAccountService)
        {
            this.patientRepository = new RepositoryWrapper<PatientRepository>(patientRepository);
            this.examinationRepository = new RepositoryWrapper<ExaminationRepository>(examinationRepository);
            this.surgeryRepository = new RepositoryWrapper<SurgeryRepository>(surgeryRepository);
            this.hospitalizationRepository =
                new RepositoryWrapper<HospitalizationRepository>(hospitalizationRepository);
            this.patientAccountService = patientAccountService;
        }

        public PatientChartDTO GetPatientChart(Patient patient)
        {
            var retPatientDTO = new PatientChartDTO();

            retPatientDTO.Examinations = examinationRepository.Repository.GetByPatient(patient);
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

        public Patient PronounceDeceased(Patient patient)
        {
            var existingPatient = patientRepository.Repository.GetByJMBG(patient.Jmbg);
            existingPatient.Status = PatientStatus.Deceased;
            patientAccountService.DeleteAccount(patientAccountService.GetAccount(existingPatient));

            return patientRepository.Repository.Update(existingPatient);
        }
    }
}