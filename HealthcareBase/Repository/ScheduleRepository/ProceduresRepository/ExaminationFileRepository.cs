// File:    ExaminationFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Class ExaminationFileRepository

using System;
using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Medication;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.MedicationRepository;
using Repository.MiscellaneousRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public class ExaminationFileRepository : GenericFileRepository<Examination, int>, ExaminationRepository
    {
        private readonly DiagnosisRepository diagnosisRepository;
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly MedicationPrescriptionRepository medicinePrescriptionRepository;
        private readonly PatientRepository patientRepository;
        private readonly ProcedureTypeRepository procedureTypeRepository;
        private readonly RoomRepository roomRepository;

        public ExaminationFileRepository(DiagnosisRepository diagnosisRepository, DoctorRepository doctorRepository,
            RoomRepository roomRepository, PatientRepository patientRepository,
            ProcedureTypeRepository procedureTypeRepository,
            MedicationPrescriptionRepository medicinePrescriptionRepository,
            string filePath) : base(filePath)
        {
            this.diagnosisRepository = diagnosisRepository;
            this.doctorRepository = doctorRepository;
            this.roomRepository = roomRepository;
            this.patientRepository = patientRepository;
            this.procedureTypeRepository = procedureTypeRepository;
            this.medicinePrescriptionRepository = medicinePrescriptionRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Examination> GetByDoctorAndDate(Doctor doctor, IEnumerable<DateTime> dates)
        {
            var examinations = new List<Examination>();

            foreach (var examination in GetAll())
                if (examination.Doctor.Equals(doctor) && dates.Contains(examination.TimeInterval.Start.Date))
                    examinations.Add(examination);

            return examinations;
        }

        public IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return GetMatching(examination =>
                examination.Doctor.Equals(doctor) && examination.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Examination> GetByRoomAndTime(Room room, TimeInterval time)
        {
            return GetMatching(examination => examination.Room.Equals(room) && examination.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Examination> GetByPatientAndTime(Patient patient, TimeInterval time)
        {
            return GetMatching(examination =>
                examination.Patient.Equals(patient) && examination.TimeInterval.Overlaps(time));
        }

        public IEnumerable<Examination> GetByPatient(Patient patient)
        {
            var examinations = new List<Examination>();
            IEnumerable<Examination> retExaminations;

            foreach (var currentExamination in GetAll())
                if (currentExamination.Patient.Equals(patient))
                    examinations.Add(currentExamination);
            retExaminations = examinations;

            return retExaminations;
        }

        protected override Examination ParseEntity(Examination entity)
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
                    entity.ReferredFrom = GetByID(entity.ReferredFrom.GetKey());
                var prescriptions = new List<MedicationPrescription>();
                foreach (var prescription in entity.Prescriptions)
                    prescriptions.Add(medicinePrescriptionRepository.GetByID(prescription.GetKey()));
                entity.Prescriptions = prescriptions;
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }

        protected override int GenerateKey(Examination entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}