// File:    HospitalizationTypeRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Interface HospitalizationTypeRepository

using Model.Schedule.Hospitalizations;
using Repository.Generics;

namespace Repository.ScheduleRepository.HospitalizationsRepository
{
    public interface HospitalizationTypeRepository : IWrappableRepository<HospitalizationType, int>
    {
    }
}