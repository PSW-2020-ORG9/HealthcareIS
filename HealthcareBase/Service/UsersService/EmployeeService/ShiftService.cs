// File:    ShiftService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class ShiftService

using System;
using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using Service.ScheduleService.ProcedureService;

namespace Service.UsersService.EmployeeService
{
    public class ShiftService
    {
        private readonly ProcedureService procedureService;
        private readonly ShiftRepository shiftRepository;

        public ShiftService(ShiftRepository shiftRepository, ProcedureService procedureService)
        {
            this.shiftRepository = shiftRepository;
            this.procedureService = procedureService;
        }

        public Shift Create(Shift shift)
        {
            var overlappingShifts = shiftRepository.GetByDoctorAndTimeOverlap(shift.Doctor, shift.TimeInterval);
            if (overlappingShifts.Count() != 0)
                throw new ScheduleViolationException();

            return shiftRepository.Create(shift);
        }

        public Shift GetByID(int id)
        {
            return shiftRepository.GetByID(id);
        }

        public IEnumerable<Shift> GetAll()
        {
            return shiftRepository.GetAll();
        }

        public void Delete(Shift shift)
        {
            var procedures = procedureService.GetByDoctorAndTime(shift.Doctor, shift.TimeInterval);
            if (procedures.Count() != 0)
                throw new ScheduleViolationException();

            shiftRepository.Delete(shift);
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
            foreach (var shift in shifts)
            {
                shift.TimeInterval = timeInterval;

                shiftRepository.Update(shift);
            }

            return shifts;
        }

        public IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time)
        {
            return shiftRepository.GetByDoctorAndTimeContaining(doctor, time);
        }

        public IEnumerable<Shift> GetByDoctor(Doctor doctor)
        {
            return shiftRepository.GetByDoctor(doctor);
        }
    }
}