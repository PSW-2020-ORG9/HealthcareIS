// File:    ProcedureTypeFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Class ProcedureTypeFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public class ProcedureTypeFileRepository : GenericFileRepository<ProcedureType, int>, ProcedureTypeRepository
    {
        private SpecialtyRepository specialtyRepository;
        private EquipmentTypeRepository equipmentTypeRepository;
        private IntegerKeyGenerator keyGenerator;

        public ProcedureTypeFileRepository(SpecialtyRepository specialtyRepository, EquipmentTypeRepository equipmentTypeRepository,
            String filePath) : base(filePath)
        {
            this.specialtyRepository = specialtyRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(ProcedureType entity)
        {
            return keyGenerator.GenerateKey();
        }

        public ProcedureType GetPatientDefault()
        {
            IEnumerable<ProcedureType> schedulableByPatient = GetMatching(type => type.SchedulableByPatient);
            if (schedulableByPatient.Count() > 0)
                return schedulableByPatient.ToList()[0];
            else
                throw new BadRequestException();
        }

        protected override ProcedureType ParseEntity(ProcedureType entity)
        {
            try
            {
                List<EquipmentType> necessaryEquipment = new List<EquipmentType>();
                foreach (EquipmentType equipment in entity.NecessaryEquipment)
                    necessaryEquipment.Add(equipmentTypeRepository.GetByID(equipment.GetKey()));
                entity.NecessaryEquipment = necessaryEquipment;
                List<Specialty> qualifiedSpecialties = new List<Specialty>();
                foreach (Specialty specialty in entity.QualifiedSpecialties)
                    qualifiedSpecialties.Add(specialtyRepository.GetByID(specialty.GetKey()));
                entity.QualifiedSpecialties = qualifiedSpecialties;
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}