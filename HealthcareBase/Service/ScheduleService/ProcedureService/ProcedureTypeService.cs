// File:    ProcedureTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ProcedureTypeService

using System.Collections.Generic;
using Model.Schedule.Procedures;
using Repository.ScheduleRepository.ProceduresRepository;

namespace Service.ScheduleService.ProcedureService
{
    public class ProcedureTypeService
    {
        private readonly ProcedureTypeRepository procedureTypeRepository;

        public ProcedureTypeService(ProcedureTypeRepository procedureTypeRepository)
        {
            this.procedureTypeRepository = procedureTypeRepository;
        }

        public ProcedureType GetPatientDefault()
        {
            return procedureTypeRepository.GetPatientDefault();
        }

        public ProcedureType GetByID(int id)
        {
            return procedureTypeRepository.GetByID(id);
        }

        public IEnumerable<ProcedureType> GetAll()
        {
            return procedureTypeRepository.GetAll();
        }

        public IEnumerable<ProcedureType> GetAllExaminationTypes()
        {
            return procedureTypeRepository.GetMatching(type => type.Kind.Equals(ProcedureKind.Examination));
        }

        public IEnumerable<ProcedureType> GetAllSurgeryTypes()
        {
            return procedureTypeRepository.GetMatching(type => type.Kind.Equals(ProcedureKind.Surgery));
        }
    }
}