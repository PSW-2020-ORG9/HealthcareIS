// File:    SpecialtyFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class SpecialtyFileRepository

using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using System;

namespace Repository.UsersRepository.EmployeesAndPatientsRepository
{
    public class SpecialtyFileRepository : GenericFileRepository<Specialty, int>, SpecialtyRepository
    {
        private IntegerKeyGenerator keyGenerator;

        public SpecialtyFileRepository(String filePath) : base(filePath)
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