// File:    DoctorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class DoctorService

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class DoctorService
    {
        private readonly RepositoryWrapper<IDoctorRepository> doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = new RepositoryWrapper<IDoctorRepository>(doctorRepository);
        }

        public Doctor GetByID(int id)
        {
            return doctorRepository.Repository.GetByID(id);
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            return doctorRepository.Repository.GetBySpecialty(specialty);
        }

        public IEnumerable<Doctor> GetAllActive()
        {
            return doctorRepository.Repository.GetMatching(doctor => doctor.Status.Equals(EmployeeStatus.Current));
        }

        public IEnumerable<Doctor> GetAll()
        {
            return doctorRepository.Repository.GetAll();
        }

        public Doctor Update(Doctor doctor)
        {
            if (doctor is null)
                throw new BadRequestException();
            return doctorRepository.Repository.Update(doctor);
        }
    }
}