using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;
using HealthcareBase.Service.ScheduleService.ProcedureService.Interface;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
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
        
        public IEnumerable<Doctor> GetAvailableByDay(DateTime date)
        {
            var shifts = _shiftWrapper.Repository.GetByShiftStart(date);
            var availableDoctors = new List<Doctor>();
            
            foreach (var shift in shifts)
            {
                var examinations =
                    _examinationWrapper.Repository.GetByDoctorAndDate(shift.Doctor,shift.TimeInterval.Start.Date);
                FindAvailableDoctors(shift, examinations, availableDoctors);
            }
            return availableDoctors;
        }

        private void FindAvailableDoctors(Shift shift, IEnumerable<Examination> examinations, ICollection<Doctor> availableDoctors)
        {
            foreach (var timeFrame in EachTimeFrame(shift.TimeInterval.Start, shift.TimeInterval.End))
            {
                if (ContainsTimeFrame(examinations, timeFrame)) continue;
                availableDoctors.Add(shift.Doctor);
                break;
            }
        }

        private static bool ContainsTimeFrame(IEnumerable<Examination> examinations, DateTime timeFrame)
        {
            return examinations.Any(examination => Overlaps(examination.TimeInterval, timeFrame));
        }
        private static bool Overlaps(TimeInterval interval, DateTime timeFrame)
        {
            return DateTime.Compare(interval.Start, timeFrame) == 0
                   && DateTime.Compare(interval.End, timeFrame.Add(Examination.MinimalTimeFrame)) == 0;
        }
        private IEnumerable<DateTime> EachTimeFrame(DateTime from, DateTime to)
        {
            for (var timeFrame = from; timeFrame < to; timeFrame = timeFrame.Add(Examination.MinimalTimeFrame))
                yield return timeFrame;
        }
    }
}