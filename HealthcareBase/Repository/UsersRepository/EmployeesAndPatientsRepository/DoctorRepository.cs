// File:    DoctorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DoctorRepository

using Model.Users.Employee;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface DoctorRepository : Repository<Doctor, int>
    {
        IEnumerable<Doctor> GetBySpecialty(Model.Users.Employee.Specialty specialty);

    }
}