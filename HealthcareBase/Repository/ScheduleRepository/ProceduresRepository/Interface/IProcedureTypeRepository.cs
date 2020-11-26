// File:    ProcedureTypeRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ProcedureTypeRepository

using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface
{
    public interface IProcedureTypeRepository : IWrappableRepository<ProcedureType, int>
    {
        ProcedureType GetPatientDefault();
    }
}