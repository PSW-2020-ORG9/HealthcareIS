using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Filters;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HospitalWebApp.Dtos;
using Microsoft.EntityFrameworkCore.Internal;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ExaminationService 
        : AbstractProcedureSchedulingService<Examination>

    {
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;
        private readonly RepositoryWrapper<IDoctorRepository> _doctorWrapper;

        public ExaminationService(
            IExaminationRepository examinationRepository,
            IShiftRepository shiftRepository, 
            IDoctorRepository doctorRepository) : base(shiftRepository)
        {
            _examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
            _doctorWrapper = new RepositoryWrapper<IDoctorRepository>(doctorRepository);
        }

        public IEnumerable<Examination> SimpleSearch(ExaminationSimpleFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());
        
        public IEnumerable<Examination> AdvancedSearch(ExaminationAdvancedFilterDto filterDto)
            => _examinationWrapper.Repository.GetMatching(filterDto.GetFilterExpression());

        public IEnumerable<Examination> GetByPatientId(int patientId)
            => _examinationWrapper.Repository.GetByPatientId(patientId);

        public override Examination GetByID(int id)
            => _examinationWrapper.Repository.GetByID(id);

        protected override Examination Create(Examination procedure)
            => _examinationWrapper.Repository.Create(procedure);

        protected override Examination Update(Examination procedure)
            => _examinationWrapper.Repository.Update(procedure);
        
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
                _examinationWrapper.Repository.GetByDoctorAndExaminationStart(procedure.DoctorId, procedure.TimeInterval.Start);
            if(examinations.Count() != 0)
                throw new ScheduleViolationException("Examination for this doctor and interval already exists.");
            
        }

        public bool Cancel(int examinationId)
        {
            var examination = _examinationWrapper.Repository.GetByID(examinationId);
            if (examination == default) return false;
            if (examination.IsCanceled) return false;
            if (!IsDateValidForCancelling(examination)) return false;
            examination.IsCanceled = true;
            return Update(examination) != default;
        }

        private bool IsDateValidForCancelling(Examination examination)
            => DateTime.Now.CompareTo(examination.TimeInterval.Start.AddDays(-2)) < 0;


        private const int RecommendationBatchSize = 5;
// Recommendations
        public List<RecommendationDto> Recommend(RecommendationRequestDto dto)
        {
            if (dto.TimeInterval == null) return null;
            return dto.Preference == RecommendationPreference.Time ? RecommendWithTime(dto) : RecommendWithDoctor(dto);
        }

        private List<RecommendationDto> RecommendWithTime(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto.DoctorId, dto.TimeInterval));
            if (recommendations.Count < RecommendationBatchSize)
                recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto));
            
            return recommendations;
        }

        private List<RecommendationDto> RecommendWithDoctor(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto.DoctorId, dto.TimeInterval));
            if (recommendations.Count < RecommendationBatchSize)
                recommendations.AddRange(RecommendForDoctorAnyTimeInterval(dto.DoctorId, dto.TimeInterval));
            if (recommendations.Count < RecommendationBatchSize)
                recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto));

            return recommendations;
        }

        private List<RecommendationDto> RecommendAnyDoctorInTimeInterval(RecommendationRequestDto dto)
        {
            IEnumerable<Shift> allShifts = _shiftWrapper.Repository
                .GetMatching(shift =>
                    shift.Doctor.Specialties.First(specialty => specialty.SpecialtyId == dto.SpecialtyId) != default
                    && shift.TimeInterval.Start >= dto.TimeInterval.Start
                    && shift.TimeInterval.Start <= dto.TimeInterval.End
                );

            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            foreach (var shift in allShifts)
            {
                if (recommendations.Count < RecommendationBatchSize)
                    recommendations.AddRange(RecommendForShift(shift));
                else
                    break;
            }

            return recommendations;
        }

        private List<RecommendationDto> RecommendForDoctorInTimeInterval(int doctorId, TimeInterval interval)
        {
            var selectedDoctorShifts = _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(doctorId, interval).ToList();
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            foreach (var shift in selectedDoctorShifts)
            {
                if (recommendations.Count < RecommendationBatchSize)
                    recommendations.AddRange(RecommendForShift(shift));
                else
                    break;
            }

            return recommendations;
        }

        private List<RecommendationDto> RecommendForDoctorAnyTimeInterval(int doctorId, TimeInterval interval)
        {
            var selectedDoctorShifts = _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(
                doctorId,
                new TimeInterval()
                {
                    Start = (interval.Start.AddMonths(-1) < DateTime.Now ? DateTime.Now : interval.Start.AddMonths(-1)),
                    End = interval.End.AddMonths(1)
                }
            );
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            foreach (var shift in selectedDoctorShifts)
            {
                if (recommendations.Count < RecommendationBatchSize)
                    recommendations.AddRange(RecommendForShift(shift));
                else
                    break;
            }
            return recommendations;
        }

        private List<RecommendationDto> RecommendForShift(Shift shift)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            var timeSlots = GetAvailableTimeSlots(shift.DoctorId, shift.TimeInterval);
            foreach (var timeSlot in timeSlots)
            {
                var doctor = _doctorWrapper.Repository.GetByID(shift.DoctorId);
                recommendations.Add(new RecommendationDto()
                {
                    Doctor = doctor,
                    TimeInterval = timeSlot
                });
            }

            return recommendations;
        }
        
        private List<TimeInterval> GetAvailableTimeSlots(int doctorId, TimeInterval timeInterval)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            
            var start = timeInterval.Start;
            while (start <= timeInterval.End)
            {
                var exam 
                    = _examinationWrapper.Repository.GetByDoctorAndExaminationStart(doctorId, start);
                if (!exam.Any())      
                    intervals.Add(new TimeInterval(start, start.Add(Examination.TimeFrameSize)));
                start = start.Add(Examination.TimeFrameSize);
            }
            
            return intervals;
        }
    }
}
