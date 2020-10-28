// File:    SurgeryFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Class SurgeryFileRepository

using System;
using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.MiscellaneousRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public class SurgeryFileRepository : GenericFileRepository<Surgery, int>, SurgeryRepository
    {
        private readonly DiagnosisRepository diagnosisRepository;
        private readonly DoctorRepository doctorRepository;
        private readonly ExaminationRepository examinationRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly PatientRepository patientRepository;
        private readonly ProcedureTypeRepository procedureTypeRepository;
        private readonly RoomRepository roomRepository;

        public SurgeryFileRepository(DiagnosisRepository diagnosisRepository, DoctorRepository doctorRepository,
            RoomRepository roomRepository, PatientRepository patientRepository,
            ProcedureTypeRepository procedureTypeRepository, ExaminationRepository examinationRepository,
            string filePath) : base(filePath)
        {
            this.diagnosisRepository = diagnosisRepository;
            this.doctorRepository = doctorRepository;
            this.roomRepository = roomRepository;
            this.patientRepository = patientRepository;
            this.procedureTypeRepository = procedureTypeRepository;
            this.examinationRepository = examinationRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Surgery> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates)
        {
            var surgeries = new List<Surgery>();

            foreach (var surgery in GetAll())
                if (surgery.Doctor.Equals(doctor) && dates.Contains(surgery.TimeInterval.Start.Date))
                    surgeries.Add(surgery);

            return surgeries;
        }

        public IEnumerable<Surgery> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return GetMatching(surgery => surgery.Doctor.Equals(doctor) && surgery.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Surgery> GetByRoomAndTime(Room room, TimeInterval time)
        {
            return GetMatching(surgery => surgery.Room.Equals(room) && surgery.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Surgery> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            return GetMatching(surgery => surgery.Patient.Equals(patient) && surgery.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Surgery> GetByPatient(Patient patient)
        {
            var surgeries = new List<Surgery>();
            IEnumerable<Surgery> retSurgeries;

            foreach (var currentSurgery in GetAll())
                if (currentSurgery.Patient.Equals(patient))
                    surgeries.Add(currentSurgery);
            retSurgeries = surgeries;

            return retSurgeries;
        }

        protected override Surgery ParseEntity(Surgery entity)
        {
            try
            {
                if (entity.Diagnosis != null)
                    entity.Diagnosis = diagnosisRepository.GetByID(entity.Diagnosis.GetKey());
                if (entity.Doctor != null)
                    entity.Doctor = doctorRepository.GetByID(entity.Doctor.GetKey());
                if (entity.Room != null)
                    entity.Room = roomRepository.GetByID(entity.Room.GetKey());
                if (entity.Patient != null)
                    entity.Patient = patientRepository.GetByID(entity.Patient.GetKey());
                if (entity.ProcedureType != null)
                    entity.ProcedureType = procedureTypeRepository.GetByID(entity.ProcedureType.GetKey());
                if (entity.ReferredFrom != null)
                    entity.ReferredFrom = examinationRepository.GetByID(entity.ReferredFrom.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(Surgery entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}