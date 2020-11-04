// File:    DepartmentRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface DepartmentRepository

using Model.HospitalResources;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface DepartmentRepository : IWrappableRepository<Department, int>
    {
    }
}