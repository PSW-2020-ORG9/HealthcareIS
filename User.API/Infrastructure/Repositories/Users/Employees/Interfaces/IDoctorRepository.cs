// File:    DoctorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface DoctorRepository

using General.Repository;
using System.Collections.Generic;
using User.API.Model.Users.Employees.Doctors;

namespace User.API.Infrastructure.Repositories.Users.Employees.Interfaces
{
    public interface IDoctorRepository : IWrappableRepository<Doctor, int>
    {
        IEnumerable<Doctor> GetBySpecialty(Specialty specialty);
    }
}