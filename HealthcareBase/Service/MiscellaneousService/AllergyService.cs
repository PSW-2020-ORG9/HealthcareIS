// File:    AllergyService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class AllergyService

using System.Collections.Generic;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MiscellaneousRepository;

namespace HealthcareBase.Service.MiscellaneousService
{
    public class AllergyService
    {
        private readonly RepositoryWrapper<IAllergyRepository> allergyRepository;

        public AllergyService(IAllergyRepository allergyRepository)
        {
            this.allergyRepository = new RepositoryWrapper<IAllergyRepository>(allergyRepository);
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