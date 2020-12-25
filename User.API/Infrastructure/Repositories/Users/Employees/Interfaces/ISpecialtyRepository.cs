// File:    SpecialtyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface SpecialtyRepository


using User.API.Model.Users.Employees.Doctors;

namespace User.API.Infrastructure.Repositories.Users.Employees.Interfaces
{
    public interface ISpecialtyRepository : IWrappableRepository<Specialty, int>
    {
    }
}