// File:    ProcedureTypeService.cs
// Author:  Lana
// Created: 28 May 2020 12:23:43
// Purpose: Definition of Class ProcedureTypeService

using System.Collections.Generic;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.ScheduleRepository.ProceduresRepository.Interface;

namespace HealthcareBase.Service.ScheduleService.ProcedureService
{
    public class ProcedureTypeService
    {
        private readonly RepositoryWrapper<IProcedureTypeRepository> procedureTypeRepository;

        public ProcedureTypeService(IProcedureTypeRepository procedureTypeRepository)
        {
            this.procedureTypeRepository = new RepositoryWrapper<IProcedureTypeRepository>(procedureTypeRepository);
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