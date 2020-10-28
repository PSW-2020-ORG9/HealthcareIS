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
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.ScheduleService.ProcedureService
{
    public class ProcedureService
    {
        private readonly ExaminationRepository examinationRepository;
        private readonly SurgeryRepository surgeryRepository;

        public ProcedureService(ExaminationRepository examinationRepository, SurgeryRepository surgeryRepository)
        {
            this.examinationRepository = examinationRepository;
            this.surgeryRepository = surgeryRepository;
        }

        public IEnumerable<Procedure> GetAll()
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.GetAll());
            allProcedures.AddRange(surgeryRepository.GetAll());
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates)
        {
            var procedures = new List<Procedure>();
            procedures.AddRange(examinationRepository.GetByDoctorAndDate(doctor, dates));
            procedures.AddRange(surgeryRepository.GetByDoctorAndDate(doctor, dates));
            return procedures;
        }

        public IEnumerable<Procedure> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.GetByDoctorAndTime(doctor, time));
            allProcedures.AddRange(surgeryRepository.GetByDoctorAndTime(doctor, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByRoomAndTime(Room room, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.GetByRoomAndTime(room, time));
            allProcedures.AddRange(surgeryRepository.GetByRoomAndTime(room, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            var allProcedures = new List<Procedure>();
            allProcedures.AddRange(examinationRepository.GetByPatientAndTime(patient, time));
            allProcedures.AddRange(surgeryRepository.GetByPatientAndTime(patient, time));
            return allProcedures;
        }

        public IEnumerable<Procedure> GetUpcomingByPatient(Patient patient)
        {
            if (patient == null)
                throw new BadRequestException();

            var procedures = new List<Procedure>();
            procedures.AddRange(examinationRepository.GetMatching(exam => exam.Patient.Equals(patient) &&
                                                                          exam.TimeInterval.Start.Date >=
                                                                          DateTime.Now));
            procedures.AddRange(surgeryRepository.GetMatching(surg => surg.Patient.Equals(patient) &&
                                                                      surg.TimeInterval.Start.Date >= DateTime.Now));

            return procedures;
        }
    }
}