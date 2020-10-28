// File:    SpecialtyService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class SpecialtyService

using Model.Users.Employee;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;

namespace Service.UsersService.EmployeeService
{
    public class SpecialtyService
    {
        private SpecialtyRepository specialtyRepository;

        public SpecialtyService(SpecialtyRepository specialtyRepository)
        {
            this.specialtyRepository = specialtyRepository;
        }

        public Specialty GetByID(int id)
        {
            return specialtyRepository.GetByID(id);
        }

        public IEnumerable<Specialty> GetAll()
        {
            return specialtyRepository.GetAll();
        }

    }
}