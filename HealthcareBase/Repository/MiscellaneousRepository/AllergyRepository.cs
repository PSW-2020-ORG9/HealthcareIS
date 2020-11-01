// File:    AllergyRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface AllergyRepository

using Model.Miscellaneous;
using Repository.Generics;

namespace Repository.MiscellaneousRepository
{
    public interface AllergyRepository : IWrappableRepository<Allergy, int>
    {
    }
}