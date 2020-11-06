// File:    SpecialtyService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class SpecialtyService

using System.Collections.Generic;
using Model.Users.Employee;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.UsersService.EmployeeService
{
    public class SpecialtyService
    {
        private readonly RepositoryWrapper<SpecialtyRepository> specialtyRepository;

        public SpecialtyService(SpecialtyRepository specialtyRepository)
        {
            this.specialtyRepository = new RepositoryWrapper<SpecialtyRepository>(specialtyRepository);
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