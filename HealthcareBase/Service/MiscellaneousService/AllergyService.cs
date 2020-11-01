// File:    AllergyService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class AllergyService

using System.Collections.Generic;
using Model.Miscellaneous;
using Repository.Generics;
using Repository.MiscellaneousRepository;

namespace Service.MiscellaneousService
{
    public class AllergyService
    {
        private readonly RepositoryWrapper<AllergyRepository> allergyRepository;

        public AllergyService(RepositoryWrapper<AllergyRepository> allergyRepository)
        {
            this.allergyRepository = allergyRepository;
        }

        public Allergy GetByID(int id)
        {
            return allergyRepository.Repository.GetByID(id);
        }

        public IEnumerable<Allergy> GetAll()
        {
            return allergyRepository.Repository.GetAll();
        }
    }
}