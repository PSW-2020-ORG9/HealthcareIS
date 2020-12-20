// File:    SpecialtyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface SpecialtyRepository

using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface
{
    public interface ISpecialtyRepository : IWrappableRepository<Specialty, int>
    {
    }
}