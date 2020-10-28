// File:    ExaminationService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ExaminationService

using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Miscellaneous;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Users.Patient.MedicalHistory;
using Model.Utilities;
using Repository.MiscellaneousRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Service.ScheduleService.Validators;

namespace Service.ScheduleService.ProcedureService
{
    public class ExaminationService : AbstractProcedureSchedulingService<Examination>
    {
        private readonly DiagnosisRepository diagnosisRepository;
        private readonly ExaminationRepository examinationRepository;
        private readonly PatientRepository patientRepository;

        public ExaminationService(ExaminationRepository examinationRepository, DiagnosisRepository diagnosisRepository,
            PatientRepository patientRepository, NotificationService.NotificationService notificationService,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit) :
            base(notificationService, scheduleValidator, procedureValidator, timeLimit)
        {
            this.examinationRepository = examinationRepository;
            this.diagnosisRepository = diagnosisRepository;
            this.patientRepository = patientRepository;
        }

        public override Examination GetByID(int id)
        {
            return examinationRepository.GetByID(id);
        }

        public IEnumerable<Examination> GetAll()
        {
            return examinationRepository.GetAll();
        }

        public IEnumerable<Examination> GetByDate(DateTime date)
        {
            return examinationRepository.GetMatching(examination =>
                examination.TimeInterval.Start.Date.Equals(date.Date));
        }

        public IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return examinationRepository.GetByDoctorAndTime(doctor, time);
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
            return examinationRepository.Update(updatedExamination);
        }

        private void RecordDiagnosisInPatientHistory(Patient patient, Diagnosis diagnosis)
        {
            patient.MedicalHistory.PersonalHistory.AddDiagnosis(new DiagnosisDetails
            {
                Diagnosis = diagnosis,
                DiscoveredAtAge = patient.Age
            });
            patientRepository.Update(patient);
        }

        private void ValidateAnamnesisAndDiagnosis(AnamnesisAndDiagnosisDTO anamnesisAndDiagnosis)
        {
            if (anamnesisAndDiagnosis.Examination is null)
                throw new BadRequestException();
            if (anamnesisAndDiagnosis.Diagnosis is null)
                throw new BadRequestException();

            anamnesisAndDiagnosis.Examination =
                examinationRepository.GetByID(anamnesisAndDiagnosis.Examination.GetKey());
            anamnesisAndDiagnosis.Diagnosis = diagnosisRepository.GetByID(anamnesisAndDiagnosis.Diagnosis.GetKey());

            if (anamnesisAndDiagnosis.Examination.TimeInterval.Start > DateTime.Now)
                throw new TimingException();
        }

        protected override Examination Create(Examination procedure)
        {
            return examinationRepository.Create(procedure);
        }

        protected override Examination Update(Examination procedure)
        {
            return examinationRepository.Update(procedure);
        }

        protected override void Delete(Examination procedure)
        {
            examinationRepository.Delete(procedure);
        }
    }
}