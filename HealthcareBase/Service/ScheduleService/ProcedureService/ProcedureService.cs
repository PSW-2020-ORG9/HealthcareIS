// File:    ProcedureService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ProcedureService

using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.ScheduleService.ProcedureService
{
    public class ProcedureService
    {
        private readonly RepositoryWrapper<ExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<SurgeryRepository> surgeryRepository;

        public ProcedureService(
            ExaminationRepository examinationRepository,
            SurgeryRepository surgeryRepository)
        {
            this.examinationRepository = new RepositoryWrapper<ExaminationRepository>(examinationRepository);
            this.surgeryRepository = new RepositoryWrapper<SurgeryRepository>(surgeryRepository);
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