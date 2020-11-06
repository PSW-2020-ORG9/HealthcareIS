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

        public ProcedureType GetPatientDefault()
        {
            return procedureTypeRepository.Repository.GetPatientDefault();
        }

        public ProcedureType GetByID(int id)
        {
            return procedureTypeRepository.Repository.GetByID(id);
        }

        public IEnumerable<ProcedureType> GetAll()
        {
            return procedureTypeRepository.Repository.GetAll();
        }

        public IEnumerable<ProcedureType> GetAllExaminationTypes()
        {
            return procedureTypeRepository.Repository.GetMatching(type => type.Kind.Equals(ProcedureKind.Examination));
        }

        public IEnumerable<ProcedureType> GetAllSurgeryTypes()
        {
            return procedureTypeRepository.Repository.GetMatching(type => type.Kind.Equals(ProcedureKind.Surgery));
        }
    }
}