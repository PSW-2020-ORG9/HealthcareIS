// File:    SpecialtyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface SpecialtyRepository

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface SpecialtyRepository : IWrappableRepository<Specialty, int>
    {
    }
}