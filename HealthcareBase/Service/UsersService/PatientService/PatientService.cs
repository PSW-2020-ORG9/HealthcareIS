// File:    PatientService.cs
// Author:  Win 10
// Created: 27 May 2020 19:14:10
// Purpose: Definition of Class PatientService

using Model.CustomExceptions;
using Model.Users.Patient;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.ScheduleRepository.HospitalizationsRepository;

namespace Service.UsersService.PatientService
{
    public class PatientService
    {
        private PatientRepository patientRepository;
        private ExaminationRepository examinationRepository;
        private SurgeryRepository surgeryRepository;
        private HospitalizationRepository hospitalizationRepository;
        private PatientAccountService patientAccountService;

        public PatientService(PatientRepository patientRepository, ExaminationRepository examinationRepository, 
            SurgeryRepository surgeryRepository, HospitalizationRepository hospitalizationRepository, 
            PatientAccountService patientAccountService)
        {
            this.patientRepository = patientRepository;
            this.examinationRepository = examinationRepository;
            this.surgeryRepository = surgeryRepository;
            this.hospitalizationRepository = hospitalizationRepository;
            this.patientAccountService = patientAccountService;
        }

        public PatientChartDTO GetPatientChart(Patient patient)
        {
            PatientChartDTO retPatientDTO = new PatientChartDTO();

            retPatientDTO.Examinations = examinationRepository.GetByPatient(patient);
            retPatientDTO.Surgeries = surgeryRepository.GetByPatient(patient);
            retPatientDTO.Hospitalizations = hospitalizationRepository.GetByPatient(patient);

            return retPatientDTO;
        }

        public Patient GetByID(int id)
        {
            return patientRepository.GetByID(id);
        }

        public IEnumerable<Patient> GetAllActive()
        {
            return patientRepository.GetMatching(patient => patient.Status == PatientStatus.Alive);
        }
       

        public Patient Create(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            return patientRepository.Create(patient);
        }

        public Patient Update(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            return patientRepository.Update(patient);
        }

        public Patient PronounceDeceased(Patient patient)
        {
            var existingPatient = patientRepository.GetByJMBG(patient.Jmbg);
            existingPatient.Status = PatientStatus.Deceased;
            patientAccountService.DeleteAccount(patientAccountService.GetAccount(existingPatient));

            return patientRepository.Update(existingPatient);
        }

    }
}