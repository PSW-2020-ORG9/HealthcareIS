// File:    DoctorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class DoctorService

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository.Interface;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public class DoctorService : IDoctorService
    {
        private readonly RepositoryWrapper<IDoctorRepository> doctorRepository;
        private const int REGULAR_DOCTOR_DEPARTMENT_ID = 1;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = new RepositoryWrapper<IDoctorRepository>(doctorRepository);
        }

        public Doctor GetByID(int id)
        {
            return doctorRepository.Repository.GetByID(id);
        }

        public IEnumerable<Doctor> GetBySpecialty(int specialtyId)
        {
            return doctorRepository.Repository.GetMatching(doctor => 
                doctor.Specialties.First(
                    specialty => specialty.SpecialtyId == specialtyId) != default);
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

        public IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId)
        {
            return doctorRepository.Repository.GetColumnsForMatching(
                condition: doctor => doctor.Id != 0 && doctor.DepartmentId == departmentId,
                selection: doctor => new DoctorDto()
                {
                    DoctorId = doctor.Id,
                    Name = doctor.Person.Name,
                    Surname = doctor.Person.Surname,
                    SpecialtyId = doctor.DepartmentId
                }
                );
        }

        public IEnumerable<DoctorDto> GetAllSpecialists()
        {
            return doctorRepository.Repository.GetColumnsForMatching(
                condition: doctor => doctor.DepartmentId != REGULAR_DOCTOR_DEPARTMENT_ID,
                selection: doctor => new DoctorDto()
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