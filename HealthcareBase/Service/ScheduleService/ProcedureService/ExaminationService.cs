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
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MiscellaneousRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationService : AbstractProcedureSchedulingService<Examination>
    {
        private readonly RepositoryWrapper<IDiagnosisRepository> _diagnosisWrapper;
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IPatientRepository> _patientWrapper;

        public ExaminationService(
            IExaminationRepository examinationRepository,
            IDiagnosisRepository diagnosisRepository,
            IPatientRepository patientRepository,
            ProcedureScheduleComplianceValidator scheduleValidator, ProcedureValidator procedureValidator,
            TimeSpan timeLimit
        ) : base(scheduleValidator, procedureValidator, timeLimit)
        {
            this._examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            this._diagnosisWrapper = new RepositoryWrapper<IDiagnosisRepository>(diagnosisRepository);
            this._patientWrapper = new RepositoryWrapper<IPatientRepository>(patientRepository);
        }

        public IEnumerable<Examination> GetByDoctorCredentials(DoctorCredentialsDto doctorCredentialsDto)
        {
            return _examinationWrapper.Repository.GetMatching(doctorCredentialsDto.GetFilterExpression());
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