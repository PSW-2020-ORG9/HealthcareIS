// File:    ExaminationService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ExaminationService

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HealthcareBase.Model.Filters;
using Model.CustomExceptions;
using Model.Miscellaneous;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Patient;
using Model.Users.Patient.MedicalHistory;
using Model.Utilities;
using Repository.Generics;
using Repository.MiscellaneousRepository;
using Repository.ScheduleRepository.ProceduresRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Service.ScheduleService.Validators;

namespace Service.ScheduleService.ProcedureService
{
    public class ExaminationService : AbstractProcedureSchedulingService<Examination>
    {
        private readonly RepositoryWrapper<DiagnosisRepository> _diagnosisWrapper;
        private readonly RepositoryWrapper<ExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<PatientRepository> _patientWrapper;

        public ExaminationService(
            ExaminationRepository examinationRepository,
            DiagnosisRepository diagnosisRepository,
            PatientRepository patientRepository,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit
        ) : base(scheduleValidator, procedureValidator, timeLimit)
        {
            this._examinationWrapper = new RepositoryWrapper<ExaminationRepository>(examinationRepository);
            this._diagnosisWrapper = new RepositoryWrapper<DiagnosisRepository>(diagnosisRepository);
            this._patientWrapper = new RepositoryWrapper<PatientRepository>(patientRepository);
        }

        public IEnumerable<Examination> GetByDoctorCredentials(ExaminationSimpleFilterDto examinationSimpleFilterDto)
        {
            return _examinationWrapper.Repository.GetMatching(examinationSimpleFilterDto.GetFilterExpression());
        }

        public override Examination GetByID(int id)
        {
            return _examinationWrapper.Repository.GetByID(id);
        }

        public IEnumerable<Examination> GetAll()
        {
            return _examinationWrapper.Repository.GetAll();
        }

        public IEnumerable<Examination> GetByDate(DateTime date)
        {
            return _examinationWrapper.Repository.GetMatching(examination =>
                examination.TimeInterval.Start.Date.Equals(date.Date));
        }

        public IEnumerable<Examination> GetByDoctorAndTime(Doctor doctor, TimeInterval time)
        {
            return _examinationWrapper.Repository.GetByDoctorAndTime(doctor, time);
        }

        public Examination RecordAnamnesisAndDiagnosos(AnamnesisAndDiagnosisDTO anamnesisAndDiagnosis)
        {
            throw new NotImplementedException();
        }

        private void RecordDiagnosisInPatientHistory(Patient patient, Diagnosis diagnosis)
        {
            throw new NotImplementedException();
        }

        private void ValidateAnamnesisAndDiagnosis(AnamnesisAndDiagnosisDTO anamnesisAndDiagnosis)
        {
            if (anamnesisAndDiagnosis.Examination is null)
                throw new BadRequestException();
            if (anamnesisAndDiagnosis.Diagnosis is null)
                throw new BadRequestException();

            anamnesisAndDiagnosis.Examination =
                _examinationWrapper.Repository.GetByID(anamnesisAndDiagnosis.Examination.GetKey());
            anamnesisAndDiagnosis.Diagnosis =
                _diagnosisWrapper.Repository.GetByID(anamnesisAndDiagnosis.Diagnosis.GetKey());

            if (anamnesisAndDiagnosis.Examination.TimeInterval.Start > DateTime.Now)
                throw new TimingException();
        }

        protected override Examination Create(Examination procedure)
        {
            return _examinationWrapper.Repository.Create(procedure);
        }

        protected override Examination Update(Examination procedure)
        {
            return _examinationWrapper.Repository.Update(procedure);
        }

        protected override void Delete(Examination procedure)
        {
            _examinationWrapper.Repository.Delete(procedure);
        }
    }
}