// File:    DepartmentFileRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Class DepartmentFileRepository

using Model.HospitalResources;
using Model.Utilities;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public class DepartmentFileRepository : GenericFileRepository<Department, int>, DepartmentRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public DepartmentFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Department entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Department ParseEntity(Department entity)
        {
            return entity;
        }
    }
}