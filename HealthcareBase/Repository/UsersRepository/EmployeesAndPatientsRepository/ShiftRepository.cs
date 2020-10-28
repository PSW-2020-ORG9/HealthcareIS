// File:    ShiftRepository.cs
// Author:  Lana
// Created: 27 May 2020 23:43:54
// Purpose: Definition of Interface ShiftRepository

using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface ShiftRepository : Repository<Shift, int>
    {
        IEnumerable<Shift> GetByDoctor(Doctor doctor); 

        IEnumerable<Shift> GetByDoctorAndTimeContaining(Doctor doctor, TimeInterval time);

        IEnumerable<Shift> GetByDoctorAndTimeOverlap(Doctor doctor, TimeInterval time);
    }
}