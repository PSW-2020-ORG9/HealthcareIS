// File:    MedicalConsumableRepository.cs
// Author:  Korisnik
// Created: 04 May 2020 12:43:47
// Purpose: Definition of Interface MedicalConsumableRepository

using Model.HospitalResources;
using Repository.Generics;

namespace Repository.HospitalResourcesRepository
{
    public interface MedicalConsumableRepository : Repository<MedicalConsumable, int>
    {
        bool ExistsByType(MedicalConsumableType type);
    }
}