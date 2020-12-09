using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Filters;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.PatientRecommendationService;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationService 
        : AbstractProcedureSchedulingService<Examination>

    {
        private readonly RepositoryWrapper<IExaminationRepository> _wrapper;
        private IRecommendationStrategy _strategy;

        public ExaminationService(
            IExaminationRepository examinationRepository,
            IShiftRepository shiftRepository) : base(shiftRepository)
        {
            _wrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
        }

        public IEnumerable<Examination> SimpleSearch(ExaminationSimpleFilterDto filterDto)
            => _wrapper.Repository.GetMatching(filterDto.GetFilterExpression());
        
        public IEnumerable<Examination> AdvancedSearch(ExaminationAdvancedFilterDto filterDto)
            => _wrapper.Repository.GetMatching(filterDto.GetFilterExpression());

        public IEnumerable<Examination> GetByPatientId(int patientId)
            => _wrapper.Repository.GetByPatientId(patientId);

        public override Examination GetByID(int id)
            => _wrapper.Repository.GetByID(id);

        protected override Examination Create(Examination procedure)
            => _wrapper.Repository.Create(procedure);

        protected override Examination Update(Examination procedure)
            => _wrapper.Repository.Update(procedure);
        
        protected override void ValidateProcedure(Examination procedure)
        {
            if(procedure == null)
                throw new NullReferenceException();
        }

        protected override void ValidateForScheduling(Examination procedure)
        {
            ValidateInterval(procedure);
            ValidateTimeConstraint(procedure);
            
        }

        private static void ValidateTimeConstraint(Examination procedure)
        {
            double difference = (procedure.TimeInterval.Start - DateTime.Now).TotalHours;
            if (difference <= Examination.TimeConstraint.TotalHours)
                throw new ScheduleViolationException("Examination cannot be scheduled because it violates time constraint.");
        }

        private void ValidateInterval(Procedure procedure)
        {
            // TODO Check if procedure start is valid according to Examination.TimeFrameSize
            var examinations =
                _wrapper.Repository.GetByDoctorAndExaminationStart(procedure.DoctorId, procedure.TimeInterval.Start);
            if(examinations.Count() != 0)
                throw new ScheduleViolationException("Examination for this doctor and interval already exists.");
            
        }

        public bool Cancel(int examinationId)
        {
            var examination = _wrapper.Repository.GetByID(examinationId);
            if (examination == default) return false;
            if (examination.IsCanceled) return false;
            if (!IsDateValidForCancelling(examination)) return false;
            examination.IsCanceled = true;
            return Update(examination) != default;
        }

        private bool IsDateValidForCancelling(Examination examination)
            => DateTime.Now.CompareTo(examination.TimeInterval.Start.AddDays(2)) > 0;
    }
}