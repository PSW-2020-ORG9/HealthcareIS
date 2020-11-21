// File:    ExaminationService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ExaminationService

using System;
using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MiscellaneousRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationService : AbstractProcedureSchedulingService<Examination>
    {
        private readonly RepositoryWrapper<DiagnosisRepository> diagnosisRepository;
        private readonly RepositoryWrapper<ExaminationRepository> examinationRepository;
        private readonly RepositoryWrapper<PatientRepository> patientRepository;

        public ExaminationService(
            ExaminationRepository examinationRepository,
            DiagnosisRepository diagnosisRepository,
            PatientRepository patientRepository,
            NotificationService.NotificationService notificationService,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit
        ) : base(notificationService, scheduleValidator, procedureValidator, timeLimit)
        {
            this.examinationRepository = new RepositoryWrapper<ExaminationRepository>(examinationRepository);
            this.diagnosisRepository = new RepositoryWrapper<DiagnosisRepository>(diagnosisRepository);
            this.patientRepository = new RepositoryWrapper<PatientRepository>(patientRepository);
        }

        public override Examination GetByID(int id)
        {
            return examinationRepository.Repository.GetByID(id);
        }

        public IEnumerable<Examination> GetAll()
        {
            return examinationRepository.Repository.GetAll();
        }

        public IEnumerable<Examination> GetByDate(DateTime date)
        {
            return examinationRepository.Repository.GetMatching(examination =>
                examination.TimeInterval.Start.Date.Equals(date.Date));
        }

        public IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return examinationRepository.Repository.GetByDoctorAndTime(doctor, time);
        }

        public Examination RecordAnamnesisAndDiagnosos(AnamnesisAndDiagnosisDTO anamnesisAndDiagnosis)
        {
            if (anamnesisAndDiagnosis is null)
                throw new BadRequestException();
            ValidateAnamnesisAndDiagnosis(anamnesisAndDiagnosis);
            var updatedExamination = anamnesisAndDiagnosis.Examination;
            updatedExamination.Diagnosis = anamnesisAndDiagnosis.Diagnosis;
            if (anamnesisAndDiagnosis.Anamnesis != null && !anamnesisAndDiagnosis.Anamnesis.Equals(""))
                updatedExamination.Anamnesis = anamnesisAndDiagnosis.Anamnesis;
            RecordDiagnosisInPatientHistory(updatedExamination.Patient, anamnesisAndDiagnosis.Diagnosis);
            return examinationRepository.Repository.Update(updatedExamination);
        }

        private void RecordDiagnosisInPatientHistory(Patient patient, Diagnosis diagnosis)
        {
            patient.MedicalHistory.PersonalHistory.AddDiagnosis(new DiagnosisDetails
            {
                Diagnosis = diagnosis,
                DiscoveredAtAge = patient.Age
            });
            patientRepository.Repository.Update(patient);
        }

        private void ValidateAnamnesisAndDiagnosis(AnamnesisAndDiagnosisDTO anamnesisAndDiagnosis)
        {
            if (anamnesisAndDiagnosis.Examination is null)
                throw new BadRequestException();
            if (anamnesisAndDiagnosis.Diagnosis is null)
                throw new BadRequestException();

            anamnesisAndDiagnosis.Examination =
                examinationRepository.Repository.GetByID(anamnesisAndDiagnosis.Examination.GetKey());
            anamnesisAndDiagnosis.Diagnosis =
                diagnosisRepository.Repository.GetByID(anamnesisAndDiagnosis.Diagnosis.GetKey());

            if (anamnesisAndDiagnosis.Examination.TimeInterval.Start > DateTime.Now)
                throw new TimingException();
        }

        protected override Examination Create(Examination procedure)
        {
            return examinationRepository.Repository.Create(procedure);
        }

        protected override Examination Update(Examination procedure)
        {
            return examinationRepository.Repository.Update(procedure);
        }

        protected override void Delete(Examination procedure)
        {
            examinationRepository.Repository.Delete(procedure);
        }
    }
}