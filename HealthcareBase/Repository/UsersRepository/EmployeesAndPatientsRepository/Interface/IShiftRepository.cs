// File:    ShiftRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:43:54
// Purpose: Definition of Interface ShiftRepository

using System;
using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface
{
    public interface IShiftRepository : IWrappableRepository<Shift, int>
    {
        IEnumerable<Shift> GetByTimeInterval(TimeInterval interval);
        IEnumerable<Shift> GetByDoctorAndShiftStart(int doctorId, DateTime shiftStart);
        IEnumerable<Shift> GetByShiftStart(DateTime shiftStart);
        public IEnumerable<Shift> GetByDoctorIdAndTimeInterval(int doctorId, TimeInterval interval);
        int GetAssignedRoomId(int doctorId, DateTime date);
    }
}