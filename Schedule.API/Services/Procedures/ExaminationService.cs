using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.API.Infrastructure.Repositories;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Exceptions;
using Schedule.API.Model.Filters;
using Schedule.API.Model.Procedures;
using Schedule.API.Services.Procedures.Interface;

namespace Schedule.API.Services.Procedures
{
    public class ExaminationService : AbstractProcedureSchedulingService<Examination>, IExaminationService
    {
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;

        public ExaminationService(
            IExaminationRepository examinationRepository,
            IShiftRepository shiftRepository
        ) : base(shiftRepository)
        {
            _examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
        }

        // Search 
        public IEnumerable<Examination> SimpleSearch(ExaminationSimpleFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());
        
        public IEnumerable<Examination> AdvancedSearch(ExaminationAdvancedFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());

        // CRUD
        public IEnumerable<Examination> GetByPatientId(int patientId)
            => _examinationWrapper.Repository.GetByPatientId(patientId);

        public override Examination GetByID(int id)
            => _examinationWrapper.Repository.GetByID(id);

        protected override Examination Create(Examination procedure)
            => _examinationWrapper.Repository.Create(procedure);

        protected override Examination Update(Examination procedure)
            => _examinationWrapper.Repository.Update(procedure);
        
        public bool Cancel(int examinationId)
        {
            var examination = _examinationWrapper.Repository.GetByID(examinationId);
            if (examination == default) return false;
            if (examination.IsCanceled) return false;
            if (!IsDateValidForCancelling(examination)) return false;
            examination.IsCanceled = true;
            return Update(examination) != default;
        }
        
        // Validations
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
                _examinationWrapper.Repository.GetByDoctorAndExaminationStart(procedure.DoctorId, procedure.TimeInterval.Start).Where(e => !e.IsCanceled);
            if(examinations.Count() != 0)
                throw new ScheduleViolationException("Examination for this doctor and interval already exists.");
        }

        private bool IsDateValidForCancelling(Examination examination)
            => DateTime.Now.CompareTo(examination.TimeInterval.Start.AddDays(-2)) < 0;
    }
}
