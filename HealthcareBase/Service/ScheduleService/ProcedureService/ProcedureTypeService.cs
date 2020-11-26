// File:    ProcedureTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ProcedureTypeService

using System.Collections.Generic;
using Model.Schedule.Procedures;
using Repository.Generics;
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.ScheduleService.ProcedureService
{
    public class ProcedureTypeService
    {
        private readonly RepositoryWrapper<ProcedureTypeRepository> procedureTypeRepository;

        public ProcedureTypeService(ProcedureTypeRepository procedureTypeRepository)
        {
            this.procedureTypeRepository = new RepositoryWrapper<ProcedureTypeRepository>(procedureTypeRepository);
        }

        public ProcedureDetails GetPatientDefault()
        {
            return procedureTypeRepository.Repository.GetPatientDefault();
        }

        public ProcedureDetails GetByID(int id)
        {
            return procedureTypeRepository.Repository.GetByID(id);
        }

        public IEnumerable<ProcedureDetails> GetAll()
        {
            return procedureTypeRepository.Repository.GetAll();
        }
    }
}