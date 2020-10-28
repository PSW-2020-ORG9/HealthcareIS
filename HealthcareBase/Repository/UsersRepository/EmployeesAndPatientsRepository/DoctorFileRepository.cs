// File:    DoctorFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class DoctorFileRepository

using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.UsersRepository.GeneralitiesRepository;
using System;
using System.Collections.Generic;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class DoctorFileRepository : GenericFileRepository<Doctor, int>, DoctorRepository
    {
        private SpecialtyRepository specialtyRepository;
        private DepartmentRepository departmentRepository;
        private CityRepository cityRepository;
        private CountryRepository countryRepository;
        private IntegerKeyGenerator keyGenerator;

        public DoctorFileRepository(SpecialtyRepository specialtyRepository, DepartmentRepository departmentRepository,
            CityRepository cityRepository, CountryRepository countryRepository, String filePath) : base(filePath)
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
                if (entity.Citizenship != null)
                {
                    List<Country> citizenship = new List<Country>();
                    foreach (Country country in entity.Citizenship)
                    {
                        citizenship.Add(countryRepository.GetByID(country.GetKey()));
                    }
                    entity.Citizenship = citizenship;
                }
                if (entity.CityOfResidence != null)
                {
                    entity.CityOfResidence = cityRepository.GetByID(entity.CityOfResidence.GetKey());
                }
                if (entity.Department != null)
                    entity.Department = departmentRepository.GetByID(entity.Department.GetKey());
                List<Specialty> equipmentInUse = new List<Specialty>();
                foreach (Specialty equipment in entity.Specialties)
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