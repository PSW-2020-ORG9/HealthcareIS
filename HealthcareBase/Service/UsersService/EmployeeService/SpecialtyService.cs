// File:    SpecialtyService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class SpecialtyService

using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class SpecialtyService
    {
        private readonly RepositoryWrapper<ISpecialtyRepository> specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            this.specialtyRepository = new RepositoryWrapper<ISpecialtyRepository>(specialtyRepository);
        }

        public Specialty GetByID(int id)
        {
            return specialtyRepository.Repository.GetByID(id);
        }

        public IEnumerable<Specialty> GetAll()
        {
            return specialtyRepository.Repository.GetAll();
        }
    }
}