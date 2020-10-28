// File:    SpecialtyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface SpecialtyRepository

using Model.Users.Employee;
using Repository.Generics;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public interface SpecialtyRepository : Repository<Specialty, int>
    {
    }
}