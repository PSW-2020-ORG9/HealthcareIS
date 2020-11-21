// File:    SpecialtyFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class SpecialtyFileRepository

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class SpecialtyFileRepository : GenericFileRepository<Specialty, int>, SpecialtyRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public SpecialtyFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Specialty entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Specialty ParseEntity(Specialty entity)
        {
            return entity;
        }
    }
}