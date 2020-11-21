// File:    ShiftService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class ShiftService

using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;
using HealthcareBase.Service.ScheduleService.ProcedureService;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class ShiftService
    {
        private readonly ProcedureService procedureService;
        private readonly RepositoryWrapper<ShiftRepository> shiftRepository;

        public ShiftService(
            ShiftRepository shiftRepository,
            ProcedureService procedureService)
        {
            this.shiftRepository = new RepositoryWrapper<ShiftRepository>(shiftRepository);
            this.procedureService = procedureService;
        }

        public Shift Create(Shift shift)
        {
            var overlappingShifts =
                shiftRepository.Repository.GetByDoctorAndTimeOverlap(shift.Doctor, shift.TimeInterval);
            if (overlappingShifts.Count() != 0)
                throw new ScheduleViolationException();

            return shiftRepository.Repository.Create(shift);
        }

        public Shift GetByID(int id)
        {
            return shiftRepository.Repository.GetByID(id);
        }

        public IEnumerable<Shift> GetAll()
        {
            return shiftRepository.Repository.GetAll();
        }

        public void Delete(Shift shift)
        {
            var procedures = procedureService.GetByDoctorAndTime(shift.Doctor, shift.TimeInterval);
            if (procedures.Count() != 0)
                throw new ScheduleViolationException();

            shiftRepository.Repository.Delete(shift);
        }

        public IEnumerable<Shift> EditShifts(ChangeShiftRequestDTO requestDTO)
        {
            IEnumerable<Shift> shifts = requestDTO.Shifts;
            var procedures = procedureService.GetByDoctorAndDate(requestDTO.Doctor, GetDatesFromShifts(shifts));

            foreach (var procedure in procedures) ValidateTimeInterval(requestDTO.Time, procedure);

            return UpdateTimeInterval(shifts, requestDTO.Time);
        }

        private IEnumerable<DateTime> GetDatesFromShifts(IEnumerable<Shift> shifts)
        {
            var dates = new List<DateTime>();

            foreach (var shift in shifts)
            {
                dates.Add(shift.TimeInterval.Start.Date);

                if (shift.TimeInterval.Start.Date != shift.TimeInterval.End.Date)
                    dates.Add(shift.TimeInterval.End.Date);
            }

            return dates;
        }

        private void ValidateTimeInterval(TimeInterval interval, Procedure procedure)
        {
            if (!(procedure.TimeInterval.Start > interval.Start && procedure.TimeInterval.End < interval.End))
                throw new ScheduleViolationException();
        }

        private IEnumerable<Shift> UpdateTimeInterval(IEnumerable<Shift> shifts, TimeInterval timeInterval)
        {
            var updateTimeInterval = shifts as Shift[] ?? shifts.ToArray();
            updateTimeInterval.ToList().ForEach(shift =>
            {
                shift.TimeInterval = timeInterval;

                shiftRepository.Repository.Update(shift);
            });

            return updateTimeInterval;
        }

        public IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time)
        {
            return shiftRepository.Repository.GetByDoctorAndTimeContaining(doctor, time);
        }

        public IEnumerable<Shift> GetByDoctor(Doctor doctor)
        {
            return shiftRepository.Repository.GetByDoctor(doctor);
        }
    }
}