// File:    DoctorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DoctorRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface DoctorRepository : IWrappableRepository<Doctor, int>
    {
        IEnumerable<Doctor> GetBySpecialty(Specialty specialty);
    }
}