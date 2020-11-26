// File:    AllergyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface AllergyRepository

using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.MiscellaneousRepository
{
    public interface IAllergyRepository : IWrappableRepository<Allergy, int>
    {
    }
}