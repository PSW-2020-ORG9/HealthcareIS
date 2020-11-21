// File:    HospitalizationTypeFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Class HospitalizationTypeFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Schedule.Hospitalizations;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.HospitalResourcesRepository;

namespace HealthcareBase.Repository.ScheduleRepository.HospitalizationsRepository
{
    public class HospitalizationTypeFileRepository : GenericFileRepository<HospitalizationType, int>,
        HospitalizationTypeRepository
    {
        private readonly DepartmentRepository departmentRepository;
        private readonly EquipmentTypeRepository equipmentTypeRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public HospitalizationTypeFileRepository(EquipmentTypeRepository equipmentTypeRepository,
            DepartmentRepository departmentRepository, string filePath) : base(filePath)
        {
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.departmentRepository = departmentRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        protected override int GenerateKey(HospitalizationType entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override HospitalizationType ParseEntity(HospitalizationType entity)
        {
            try
            {
                var appropriateDepartments = new List<Department>();
                foreach (var department in entity.AppropriateDepartments)
                    appropriateDepartments.Add(departmentRepository.GetByID(department.GetKey()));
                entity.AppropriateDepartments = appropriateDepartments;
                var necessaryEquipment = new List<EquipmentType>();
                foreach (var type in entity.NecessaryEquipment)
                    necessaryEquipment.Add(equipmentTypeRepository.GetByID(type.GetKey()));
                entity.NecessaryEquipment = necessaryEquipment;
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}