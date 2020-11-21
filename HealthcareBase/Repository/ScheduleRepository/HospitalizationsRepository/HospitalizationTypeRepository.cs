// File:    HospitalizationTypeRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Interface HospitalizationTypeRepository

using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository
{
    public interface HospitalizationTypeRepository : IWrappableRepository<HospitalizationType, int>
    {
    }
}