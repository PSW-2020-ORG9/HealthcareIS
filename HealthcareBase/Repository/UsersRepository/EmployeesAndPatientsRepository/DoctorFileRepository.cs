// File:    DoctorFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class DoctorFileRepository

using System;
using System.Collections.Generic;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.UsersRepository.GeneralitiesRepository;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class DoctorFileRepository : GenericFileRepository<Doctor, int>, DoctorRepository
    {
        private readonly CityRepository cityRepository;
        private readonly CountryRepository countryRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly SpecialtyRepository specialtyRepository;

        public DoctorFileRepository(SpecialtyRepository specialtyRepository, DepartmentRepository departmentRepository,
            CityRepository cityRepository, CountryRepository countryRepository, string filePath) : base(filePath)
        {
            this.specialtyRepository = specialtyRepository;
            this.departmentRepository = departmentRepository;
            this.cityRepository = cityRepository;
            this.countryRepository = countryRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<Doctor> GetBySpecialty(Specialty specialty)
        {
            throw new NotImplementedException();
        }

        protected override int GenerateKey(Doctor entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Doctor ParseEntity(Doctor entity)
        {
            try
            {
                if (entity.Citizenships != null)
                {
                    var citizenship = new List<Country>();
                    foreach (var country in entity.Citizenships)
                        citizenship.Add(countryRepository.GetByID(country.GetKey()));
                }

                if (entity.CityOfResidence != null)
                    entity.CityOfResidence = cityRepository.GetByID(entity.CityOfResidence.GetKey());
                if (entity.Department != null)
                    entity.Department = departmentRepository.GetByID(entity.Department.GetKey());
                var equipmentInUse = new List<Specialty>();
                foreach (var equipment in entity.Specialties)
                    equipmentInUse.Add(specialtyRepository.GetByID(equipment.GetKey()));
                entity.Specialties = equipmentInUse;
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}