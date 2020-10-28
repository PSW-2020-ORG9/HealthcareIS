// File:    DoctorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class DoctorService

using Model.CustomExceptions;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.UsersService.EmployeeService
{
    public class DoctorService
    {
        private DoctorRepository doctorRepository;

        public DoctorService(DoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public IEnumerable<Doctor> GetQualified(ProcedureType procedureType)
        {
            List<Doctor> qualified = new List<Doctor>();
            foreach (Doctor doctor in doctorRepository.GetAll())
            {
                if (!doctor.Status.Equals(EmployeeStatus.Current))
                    continue;
                IEnumerable<Specialty> matchingSpeicalties =
                    procedureType.QualifiedSpecialties.Intersect(doctor.Specialties);
                if (matchingSpeicalties.Count() == 0)
                    continue;

                qualified.Add(doctor);
            }

            return qualified;
        }

        public Doctor GetByID(int id)
        {
            return doctorRepository.GetByID(id);
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            return doctorRepository.GetBySpecialty(specialty);
        }

        public IEnumerable<Doctor> GetAllActive()
        {
            return doctorRepository.GetMatching(doctor => doctor.Status.Equals(EmployeeStatus.Current));
        }

        public IEnumerable<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Doctor Update(Doctor doctor)
        {
            if (doctor is null)
                throw new BadRequestException();
            return doctorRepository.Update(doctor);
        }

    }
}