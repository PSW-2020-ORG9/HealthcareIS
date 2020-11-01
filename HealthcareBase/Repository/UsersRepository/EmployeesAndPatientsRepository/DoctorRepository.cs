// File:    DoctorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DoctorRepository

using System.Collections.Generic;
using Model.Users.Employee;
using Repository.Generics;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface DoctorRepository : IWrappableRepository<Doctor, int>
    {
        IEnumerable<Doctor> GetBySpecialty(Specialty specialty);
    }
}