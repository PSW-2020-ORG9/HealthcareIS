// File:    ProcedureTypeFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:58:09
// Purpose: Definition of Class ProcedureTypeFileRepository

using System.Collections.Generic;
using System.Linq;
using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace Repository.ScheduleRepository.ProceduresRepository
{
    public class ProcedureTypeFileRepository : GenericFileRepository<ProcedureType, int>, ProcedureTypeRepository
    {
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly IntegerKeyGenerator keyGenerator;
        private readonly SpecialtyRepository specialtyRepository;

        public ProcedureTypeFileRepository(SpecialtyRepository specialtyRepository,
            EquipmentTypeRepository equipmentTypeRepository,
            string filePath) : base(filePath)
        {
            this.specialtyRepository = specialtyRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public ProcedureType GetPatientDefault()
        {
            var schedulableByPatient = GetMatching(type => type.SchedulableByPatient);
            if (schedulableByPatient.Count() > 0)
                return schedulableByPatient.ToList()[0];
            throw new BadRequestException();
        }

        protected override int GenerateKey(ProcedureType entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override ProcedureType ParseEntity(ProcedureType entity)
        {
            try
            {
                var necessaryEquipment = new List<EquipmentType>();
                foreach (var equipment in entity.NecessaryEquipment)
                    necessaryEquipment.Add(equipmentTypeRepository.GetByID(equipment.GetKey()));
                entity.NecessaryEquipment = necessaryEquipment;
                var qualifiedSpecialties = new List<Specialty>();
                foreach (var specialty in entity.QualifiedSpecialties)
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