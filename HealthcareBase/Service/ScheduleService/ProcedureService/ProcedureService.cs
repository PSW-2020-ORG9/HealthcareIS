// File:    ProcedureService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ProcedureService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ProcedureService
    {
        private readonly RepositoryWrapper<IExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<ISurgeryRepository> surgeryRepository;

        public ProcedureService(
            IExaminationRepository examinationRepository,
            ISurgeryRepository surgeryRepository)
        {
            this.examinationRepository = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this.surgeryRepository = new RepositoryWrapper<ISurgeryRepository>(surgeryRepository);
        }

        public IEnumerable<Procedure> GetAll()
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.Repository.GetAll());
            allProcedures.AddRange(surgeryRepository.Repository.GetAll());
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates)
        {
            var procedures = new List<Procedure>();
            procedures.AddRange(examinationRepository.Repository.GetByDoctorAndDate(doctor, dates));
            procedures.AddRange(surgeryRepository.Repository.GetByDoctorAndDate(doctor, dates));
            return procedures;
        }

        public IEnumerable<Procedure> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.Repository.GetByDoctorAndTime(doctor, time));
            allProcedures.AddRange(surgeryRepository.Repository.GetByDoctorAndTime(doctor, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByRoomAndTime(Room room, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.Repository.GetByRoomAndTime(room, time));
            allProcedures.AddRange(surgeryRepository.Repository.GetByRoomAndTime(room, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.Repository.GetByPatientAndTime(patient, time));
            allProcedures.AddRange(surgeryRepository.Repository.GetByPatientAndTime(patient, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetUpcomingByPatient(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            var procedures = new List<Procedure>();
            procedures.AddRange(examinationRepository.Repository.GetMatching(exam => exam.Patient.Equals(patient) &&
                exam.TimeInterval.Start.Date >=
                DateTime.Now));
            procedures.AddRange(surgeryRepository.Repository.GetMatching(surg => surg.Patient.Equals(patient) &&
                surg.TimeInterval.Start.Date >= DateTime.Now));

            return procedures;
        }
    }
}