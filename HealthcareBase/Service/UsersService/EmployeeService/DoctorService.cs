// File:    DoctorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class DoctorService

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Service.UsersService.EmployeeService
{
    public class DoctorService
    {
        private readonly RepositoryWrapper<DoctorRepository> doctorRepository;

        public DoctorService(RepositoryWrapper<DoctorRepository> doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public IEnumerable<Doctor> GetQualified(ProcedureType procedureType)
        {
            var qualified = new List<Doctor>();
            foreach (var doctor in doctorRepository.Repository.GetAll())
            {
                if (!doctor.Status.Equals(EmployeeStatus.Current))
                    continue;
                var matchingSpeicalties =
                    procedureType.QualifiedSpecialties.Intersect(doctor.Specialties);
                if (matchingSpeicalties.Count() == 0)
                    continue;

                qualified.Add(doctor);
            }

            return qualified;
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