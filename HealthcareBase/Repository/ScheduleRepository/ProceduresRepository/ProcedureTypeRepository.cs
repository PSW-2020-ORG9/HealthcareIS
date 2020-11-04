// File:    ProcedureTypeRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ProcedureTypeRepository

using Model.Schedule.Procedures;
using Repository.Generics;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public interface ProcedureTypeRepository : IWrappableRepository<ProcedureType, int>
    {
        ProcedureType GetPatientDefault();
    }
}