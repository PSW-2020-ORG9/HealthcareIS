using System.Collections.Generic;
using System.Runtime.InteropServices;
using HealthcareBase.Model.Filters;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Service.ScheduleService.PatientRecommendationService;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationService 
        : AbstractProcedureSchedulingService<Examination>

    {
        private readonly RepositoryWrapper<IExaminationRepository> _wrapper;
        private IRecommendationStrategy _strategy;

        public ExaminationService(IExaminationRepository examinationRepository)
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
            throw new System.NotImplementedException();
        }

        public bool Cancel(int examinationId)
        {
            var examination = _wrapper.Repository.GetByID(examinationId);
            if (examination == default) return false;
            if (examination.IsCanceled) return false;
            examination.IsCanceled = true;
            return Update(examination) != default;
        }
    }
}