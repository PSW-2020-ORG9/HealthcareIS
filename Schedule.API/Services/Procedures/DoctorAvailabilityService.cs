using System;
using System.Collections.Generic;
using System.Linq;
using Schedule.API.Infrastructure.Repositories.Procedures.Interfaces;
using Schedule.API.Infrastructure.Repositories.Shifts;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Procedures;
using Schedule.API.Model.Shifts;
using Schedule.API.Model.Utilities;
using Schedule.API.Services.Procedures.Interface;
using User.API.Infrastructure.Repositories;

namespace Schedule.API.Services.Procedures
{
    public class DoctorAvailabilityService:IDoctorAvailabilityService
    {
        private readonly RepositoryWrapper<IShiftRepository> _shiftWrapper;
        private readonly RepositoryWrapper<IExaminationRepository> _examinationWrapper;

        public DoctorAvailabilityService(IShiftRepository shiftRepository,
            IExaminationRepository examinationRepository)
        {
            _shiftWrapper = new RepositoryWrapper<IShiftRepository>(shiftRepository);
            _examinationWrapper = new RepositoryWrapper<IExaminationRepository>(examinationRepository);
        }

       
        /// <summary>
        /// Gets all available intervals for scheduling
        /// </summary>
        /// <param name="DoctorId">For a certain doctor</param>
        /// <param name="date">On a certain day</param>
        /// <returns></returns>
        public IEnumerable<TimeInterval> GetAvailableIntervals(int doctorId, DateTime date)
        {
            var intervals = new List<TimeInterval>();
            var shifts =
                _shiftWrapper.Repository.GetByDoctorAndShiftStart(doctorId, date);
            foreach (var shift in shifts)
            {
                var examinations =
                    _examinationWrapper.Repository.GetByDoctorAndDate(shift.Doctor.Id,shift.TimeInterval.Start.Date).Where(e => !e.IsCanceled);
                InsertAvailableIntervals(shift,examinations,intervals);
            }

            return intervals;
        }

        private void InsertAvailableIntervals(Shift shift, IEnumerable<Examination> examinations, ICollection<TimeInterval> intervals)
        {
            foreach (var timeFrame in EachTimeFrameStart(shift.TimeInterval.Start, shift.TimeInterval.End))
            {
                if (IsDuringTimeFrame(examinations, timeFrame)) continue;
                
                intervals.Add(new TimeInterval(timeFrame,timeFrame.Add(Examination.TimeFrameSize)));
            }
        }

        /// <summary>
        /// Finds all available doctors on certain day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<Doctor> GetAvailableByDay(DateTime date)
        {
            var shifts = _shiftWrapper.Repository.GetByShiftStart(date);
            var availableDoctors = new List<Doctor>();
            
            foreach (var shift in shifts)
            {
                var examinations =
                    _examinationWrapper.Repository.GetByDoctorAndDate(shift.Doctor.Id,shift.TimeInterval.Start.Date);
                FindAvailableDoctors(shift, examinations, availableDoctors);
            }
            return availableDoctors;
        }

        private void FindAvailableDoctors(Shift shift, IEnumerable<Examination> examinations, ICollection<Doctor> availableDoctors)
        {
            foreach (var timeFrame in EachTimeFrameStart(shift.TimeInterval.Start, shift.TimeInterval.End))
            {
                if (IsDuringTimeFrame(examinations, timeFrame)) continue;
                availableDoctors.Add(shift.Doctor);
                break;
            }
        }
        /// <summary>
        /// Checks if examination in passed time period exists
        /// </summary>
        /// <param name="examinations"></param>
        /// <param name="timeFrame"></param>
        /// <returns></returns>
        private static bool IsDuringTimeFrame(IEnumerable<Examination> examinations, DateTime timeFrame)
        {
            return examinations.Any(examination => Overlaps(examination.TimeInterval, timeFrame));
        }
        
        private static bool Overlaps(TimeInterval interval, DateTime timeFrame)
        {
            return DateTime.Compare(interval.Start, timeFrame) == 0
                   && DateTime.Compare(interval.End, timeFrame.Add(Examination.TimeFrameSize)) == 0;
        }
        
        /// <summary>
        /// Segments time interval according to MinimalTimeFrame (Minimal examination length)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> EachTimeFrameStart(DateTime from, DateTime to)
        {
            for (var timeFrame = from; timeFrame < to; timeFrame = timeFrame.Add(Examination.TimeFrameSize))
                yield return timeFrame;
        }
    }
}