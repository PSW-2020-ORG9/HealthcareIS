// File:    MedicalConsumableTypeRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface MedicalConsumableTypeRepository

using Model.HospitalResources;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface MedicalConsumableTypeRepository : Repository<MedicalConsumableType, int>
    {
    }
}