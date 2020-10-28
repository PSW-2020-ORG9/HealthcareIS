// File:    AllergyFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class AllergyFileRepository

using Model.Miscellaneous;
using Model.Utilities;
using Repository.Generics;

namespace Repository.MiscellaneousRepository
{
    public class AllergyFileRepository : GenericFileRepository<Allergy, int>, AllergyRepository
    {
        private readonly IntegerKeyGenerator keyGenerator;

        public AllergyFileRepository(string filePath) : base(filePath)
        {
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(Allergy entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override Allergy ParseEntity(Allergy entity)
        {
            return entity;
        }
    }
}