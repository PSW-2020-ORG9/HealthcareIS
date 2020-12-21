using System;
using System.Collections.Generic;
using System.Linq;
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
                _examinationWrapper.Repository.GetByDoctorAndExaminationStart(procedure.DoctorId, procedure.TimeInterval.Start).Where(e => !e.IsCanceled);
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


        // Recommendations
        private const int RecommendationBatchSize = 5;
        private int RemainingSlots(IEnumerable<RecommendationDto> r) => RecommendationBatchSize - r.Count();
        
        public List<RecommendationDto> Recommend(RecommendationRequestDto dto)
        {
            if (dto.TimeInterval == null) return null;
            return dto.Preference == RecommendationPreference.Time ? RecommendWithTime(dto) : RecommendWithDoctor(dto);
        }

        private List<RecommendationDto> RecommendWithTime(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto, RemainingSlots(recommendations)));
            recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto, RemainingSlots(recommendations)));
            
            return recommendations;
        }

        private List<RecommendationDto> RecommendWithDoctor(RecommendationRequestDto dto)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            recommendations.AddRange(RecommendForDoctorInTimeInterval(dto, RemainingSlots(recommendations)));
            recommendations.AddRange(RecommendForDoctorAnyTimeInterval(dto, RemainingSlots(recommendations)));
            recommendations.AddRange(RecommendAnyDoctorInTimeInterval(dto, RemainingSlots(recommendations)));

            return recommendations;
        }

        private List<RecommendationDto> RecommendAnyDoctorInTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();

            IEnumerable<Shift> allShifts = _shiftWrapper.Repository
                .GetMatching(shift =>
                    shift.Doctor.Specialties.First(specialty => specialty.SpecialtyId == dto.SpecialtyId) != default
                    && shift.TimeInterval.Start >= dto.TimeInterval.Start
                    && shift.TimeInterval.Start <= dto.TimeInterval.End
                );

            return FindRecommendationsInShifts(allShifts, remainingSlots);
        }

        private List<RecommendationDto> RecommendForDoctorInTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();
            
            var selectedDoctorShifts = 
                _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(dto.DoctorId, dto.TimeInterval).ToList();

            return FindRecommendationsInShifts(selectedDoctorShifts, remainingSlots);
        }

        private List<RecommendationDto> RecommendForDoctorAnyTimeInterval(RecommendationRequestDto dto, int remainingSlots)
        {
            if (remainingSlots == 0) return new List<RecommendationDto>();
            
            var selectedDoctorShifts = _shiftWrapper.Repository.GetByDoctorIdAndTimeInterval(
                dto.DoctorId,
                new TimeInterval()
                {
                    Start = (dto.TimeInterval.Start.AddMonths(-1) < DateTime.Now ? DateTime.Now : dto.TimeInterval.Start.AddMonths(-1)),
                    End = dto.TimeInterval.End.AddMonths(1)
                }
            );
            return FindRecommendationsInShifts(selectedDoctorShifts, remainingSlots);
        }

        /// <summary>
        /// Returns a list of <see cref="RecommendationDto"/> objects which can take place within the
        /// given Shift array.
        /// Max number of returned objects is defined by <see cref="maxTimeSlots"/> argument.
        /// </summary>
        /// <param name="shifts"></param>
        /// <param name="maxTimeSlots"></param>
        /// <returns></returns>
        private List<RecommendationDto> FindRecommendationsInShifts(IEnumerable<Shift> shifts, int maxTimeSlots)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            foreach (var shift in shifts)
            {
                if (maxTimeSlots > 0)
                {
                    List<RecommendationDto> singleShiftRecommendations = RecommendForShift(shift, maxTimeSlots);
                    recommendations.AddRange(singleShiftRecommendations);
                    maxTimeSlots -= singleShiftRecommendations.Count;
                }
                else
                    break;
            }
            return recommendations;
        }

        /// <summary>
        /// Returns a list of <see cref="RecommendationDto"/> objects which can take place within the given Shift.
        /// Max number of returned Recommendations is defined by <see cref="maxSlots"/> argument.
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="maxSlots"></param>
        /// <returns></returns>
        private List<RecommendationDto> RecommendForShift(Shift shift, int maxSlots)
        {
            List<RecommendationDto> recommendations = new List<RecommendationDto>();
            
            var timeSlots = GetAvailableTimeSlots(shift.DoctorId, shift.TimeInterval, maxSlots);
            foreach (var timeSlot in timeSlots)
            {
                var doctor = _doctorWrapper.Repository.GetByID(shift.DoctorId);
                recommendations.Add(new RecommendationDto()
                {
                    Doctor = doctor,
                    TimeInterval = timeSlot,
                    RoomId = shift.AssignedExamRoomId
                });
            }

            return recommendations;
        }
        
        /// <summary>
        /// Returns a list of TimeIntervals in which the Doctor with the given ID is available for the given
        /// <see cref="TimeInterval"/> argument.
        /// Max number of returned free intervals is defined by the <see cref="maxSlots"/> argument.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="timeInterval"></param>
        /// <param name="maxSlots"></param>
        /// <returns></returns>
        private List<TimeInterval> GetAvailableTimeSlots(int doctorId, TimeInterval timeInterval, int maxSlots)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            
            var start = timeInterval.Start;
            while (start < timeInterval.End  &&  maxSlots > 0)
            {
                var exam 
                    = _examinationWrapper.Repository.GetByDoctorAndExaminationStart(doctorId, start).Where(e => !e.IsCanceled);
                if (!exam.Any())
                {
                    intervals.Add(new TimeInterval(start, start.Add(Examination.TimeFrameSize)));
                    maxSlots--;
                }
                start = start.Add(Examination.TimeFrameSize);
            }
            
            return intervals;
        }
    }
}
