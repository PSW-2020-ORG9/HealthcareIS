// File:    ShiftRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:43:54
// Purpose: Definition of Interface ShiftRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface ShiftRepository : IWrappableRepository<Shift, int>
    {
        IEnumerable<Shift> GetByDoctor(Doctor doctor);

        IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time);

        IEnumerable<Shift> GetByDoctorAndTimeOverlap(Doctor doctor, TimeInterval time);
    }
}