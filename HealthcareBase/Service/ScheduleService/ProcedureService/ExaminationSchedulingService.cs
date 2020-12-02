// File:    ExaminationService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ExaminationService


using System;
using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Filters;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MiscellaneousRepository;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.PatientRecommendationService;
using HealthcareBase.Service.ScheduleService.Validators;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationSchedulingService : AbstractProcedureSchedulingService<Examination>
    {
        private readonly RepositoryWrapper<IDiagnosisRepository> _diagnosisWrapper;
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IPatientRepository> _patientWrapper;
        private IRecommendationStrategy _strategy;

        public ExaminationSchedulingService(
            IExaminationRepository examinationRepository,
            IDiagnosisRepository diagnosisRepository,
            IPatientRepository patientRepository,
            ProcedureScheduleComplianceValidator scheduleValidator,
            TimeSpan timeLimit
        ) : base(timeLimit)
        {
            _examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            _diagnosisWrapper = new RepositoryWrapper<IDiagnosisRepository>(diagnosisRepository);
            _patientWrapper = new RepositoryWrapper<IPatientRepository>(patientRepository);
        }

        public IEnumerable<Examination> SimpleSearch(ExaminationSimpleFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());
        
        public IEnumerable<Examination> AdvancedSearch(ExaminationAdvancedFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());

        
        // Old code
        public override Examination GetByID(int id)
        {
            return _examinationWrapper.Repository.GetByID(id);
        }

        private void SetStrategy(IRecommendationStrategy strategy)
        {
            _strategy = strategy;
        }

        public Examination GetRecommendation(RecommendationPriority priority, Examination examination)
        {
            if (priority == RecommendationPriority.Doctor)
                _strategy = new PrioritiseDoctorStrategy();
            else _strategy = new PrioritiseTimeStrategy();

            return _strategy.ChooseBest(examination);
        }

        public IEnumerable<Examination> GetAll()
        {
            return _examinationWrapper.Repository.GetAll();
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

        protected override void Validate(Examination procedure)
        {
            throw new NotImplementedException(); 
        }

        protected override void ValidateProcedure(Examination procedure)
        {
            throw new NotImplementedException();
        }
    }
}