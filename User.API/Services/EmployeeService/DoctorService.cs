// File:    DoctorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class DoctorService

using General.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using User.API.DTOs;
using User.API.Infrastructure.Exceptions;
using User.API.Infrastructure.Repositories;
using User.API.Infrastructure.Repositories.Users.Employees.Interfaces;
using User.API.Model.Users.Employees;
using User.API.Model.Users.Employees.Doctors;


namespace User.API.Services.EmployeeService
{
    public class DoctorService : IDoctorService
    {
        private readonly RepositoryWrapper<IDoctorRepository> _doctorRepository;
        private const int REGULAR_DOCTOR_DEPARTMENT_ID = 1;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = new RepositoryWrapper<IDoctorRepository>(doctorRepository);
        }

        public Doctor GetByID(int id)
        {
            return _doctorRepository.Repository.GetByID(id);
        }

        public IEnumerable<Doctor> GetBySpecialty(int specialtyId)
        {
            return _doctorRepository.Repository.GetMatching(doctor => 
                doctor.Specialties.First(
                    specialty => specialty.SpecialtyId == specialtyId) != default);
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            return _doctorRepository.Repository.GetBySpecialty(specialty);
        }

        public IEnumerable<Doctor> GetAllActive()
        {
            return _doctorRepository.Repository.GetMatching(doctor => doctor.Status.Equals(EmployeeStatus.Current));
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorRepository.Repository.GetAll();
        }

        public Doctor Update(Doctor doctor)
        {
            if (doctor is null)
                throw new BadRequestException();
            return _doctorRepository.Repository.Update(doctor);
        }

        public IEnumerable<DoctorDTO> GetDoctorsByDepartment(int departmentId)
        {
            return _doctorRepository.Repository.GetColumnsForMatching(
                condition: doctor => doctor.Id != 0 && doctor.DepartmentId == departmentId,
                selection: doctor => new DoctorDTO
                {
                    DoctorId = doctor.Id,
                    Name = doctor.Person.Name,
                    Surname = doctor.Person.Surname,
                    SpecialtyId = doctor.DepartmentId
                }
                );
        }

        public IEnumerable<Doctor> Find(IEnumerable<int> doctorIds) 
            => _doctorRepository.Repository.GetMatching(doctor => doctorIds.Contains(doctor.Id));

        public IEnumerable<int> GetIdsBySpecialty(int specialtyId)
            => _doctorRepository.Repository.GetColumnsForMatching(
                condition: 
                    doctor => doctor.Specialties.Count(specialty => specialty.SpecialtyId == specialtyId) > 0,
                selection:
                    doctor => doctor.Id
            );

        public IEnumerable<int> FindIdsByCredentials(string name, string surname)
        {
            return _doctorRepository.Repository.GetColumnsForMatching(
                condition: doctor => doctor.Person.Name.Contains(name) && doctor.Person.Surname.Contains(surname),
                selection:
                doctor => doctor.Id
            );
        }

        public IEnumerable<DoctorDTO> GetAllSpecialists()
        {
            return _doctorRepository.Repository.GetColumnsForMatching(
                condition: doctor => doctor.DepartmentId != REGULAR_DOCTOR_DEPARTMENT_ID,
                selection: doctor => new DoctorDTO()
                {
                    Name = doctor.Person.Name,
                    Surname = doctor.Person.Surname,
                    DoctorId = doctor.Id,
                    DepartmentName = doctor.Department.Name
                }
                );
        }
    }
}