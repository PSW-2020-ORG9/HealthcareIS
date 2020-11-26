// File:    DoctorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DoctorRepository

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface
{
    public interface IDoctorRepository : IWrappableRepository<Doctor, int>
    {
        IEnumerable<Doctor> GetBySpecialty(Specialty specialty);
    }
}