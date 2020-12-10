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
            examination.IsCanceled = true;
            return Update(examination) != default;
        }

// Recommendations
        public RecommendationDto Recommend(RecommendationRequestDto dto)
        {
            if (dto.TimeInterval == null) return null;
            return dto.Preference == RecommendationPreference.Time ? RecommendWithTime(dto) : RecommendWithDoctor(dto);
        }

        private RecommendationDto RecommendWithTime(RecommendationRequestDto dto)
        {
            var selectedDoctorRecommendation = RecommendForDoctorInTimeInterval(dto.DoctorId, dto.TimeInterval);
            if (selectedDoctorRecommendation != null)
                return selectedDoctorRecommendation;

            return RecommendAnyDoctorInTimeInterval(dto);
        }

        private RecommendationDto RecommendWithDoctor(RecommendationRequestDto dto)
        {
            RecommendationDto inSpecifiedInterval = RecommendForDoctorInTimeInterval(dto.DoctorId, dto.TimeInterval);
            if (inSpecifiedInterval != null)
                return inSpecifiedInterval;

            RecommendationDto anyInterval = RecommendForDoctorAnyTimeInterval(dto.DoctorId, dto.TimeInterval);
            if (anyInterval != null)
                return anyInterval;

            return RecommendAnyDoctorInTimeInterval(dto);
        }

        private RecommendationDto RecommendAnyDoctorInTimeInterval(RecommendationRequestDto dto)
        {
            IEnumerable<Shift> allShifts = _shiftWrapper.Repository
                .GetMatching(shift =>
                    shift.Doctor.Specialties.First(specialty => specialty.SpecialtyId == dto.SpecialtyId) != default
                    && shift.TimeInterval.Start >= dto.TimeInterval.Start
                    && shift.TimeInterval.Start <= dto.TimeInterval.End
                );

            foreach (var shift in allShifts)
            {
                var recommendationDto = RecommendForShift(shift);
                if (recommendationDto != null)
                    return recommendationDto;
            }
            return null;
        }

        private RecommendationDto RecommendForDoctorInTimeInterval(int doctorId, TimeInterval interval)
        {
            var selectedDoctorShifts =
                _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(doctorId, interval).ToList();
            if (selectedDoctorShifts.Any())
            {
                foreach (var shift in selectedDoctorShifts)
                {
                    var recommendationDto = RecommendForShift(shift);
                    if (recommendationDto != null)
                        return recommendationDto;
                }
            }
            return null;
        }

        private RecommendationDto RecommendForDoctorAnyTimeInterval(int doctorId, TimeInterval interval)
        {
            var selectedDoctorShifts = _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(
                doctorId,
                new TimeInterval()
                {
                    Start = (interval.Start.AddMonths(-1) < DateTime.Now ? DateTime.Now : interval.Start.AddMonths(-1)),
                    End = interval.End.AddMonths(1)
                }
            );
            foreach (var shift in selectedDoctorShifts)
            {
                var timeSlot = RecommendForShift(shift);
                if (timeSlot != null)
                    return timeSlot;
            }
            return null;
        }

        private RecommendationDto RecommendForShift(Shift shift)
        {
            var timeSlot = GetFirstAvailableTimeSlot(shift.DoctorId, shift.TimeInterval);
            if (timeSlot != null)
            {
                var doctor = _doctorWrapper.Repository.GetByID(shift.DoctorId);
                return new RecommendationDto()
                {
                    Doctor = doctor,
                    TimeInterval = timeSlot
                };
            }
            return null;
        }
        
        private TimeInterval GetFirstAvailableTimeSlot(int doctorId, TimeInterval timeInterval)
        {
            var start = timeInterval.Start;
            while (start.Date <= timeInterval.End.Date)
            {
                var exam 
                    = _examinationWrapper.Repository.GetByDoctorAndExaminationStart(doctorId, start);
                if (exam.Any())
                    start = start.Add(Examination.TimeFrameSize);
                else
                    return new TimeInterval(start, start.Add(Examination.TimeFrameSize));
            }
            return null;
        }
    }
}
