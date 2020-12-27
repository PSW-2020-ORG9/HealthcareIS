// File:    ProcedureTypeRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Interface ProcedureTypeRepository

using Schedule.API.Model.Procedures;

namespace Schedule.API.Infrastructure.Repositories.Procedures.Interface
{
    public interface IProcedureTypeRepository : IWrappableRepository<ProcedureDetails, int>
    {
        ProcedureDetails GetPatientDefault();
    }
}