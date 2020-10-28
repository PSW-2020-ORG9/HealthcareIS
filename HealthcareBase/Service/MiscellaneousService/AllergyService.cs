// File:    AllergyService.cs
// Author:  Lana
// Created: 28 May 2020 12:04:04
// Purpose: Definition of Class AllergyService

using Model.Miscellaneous;
using Repository.MiscellaneousRepository;
using System;
using System.Collections.Generic;

namespace Service.MiscellaneousService
{
    public class AllergyService
    {
        private AllergyRepository allergyRepository;

        public AllergyService(AllergyRepository allergyRepository)
        {
            this.allergyRepository = allergyRepository;
        }

        public Allergy GetByID(int id)
        {
            return allergyRepository.GetByID(id);
        }

        public IEnumerable<Allergy> GetAll()
        {
            return allergyRepository.GetAll();
        }

    }
}