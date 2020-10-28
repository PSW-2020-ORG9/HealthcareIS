// File:    HospitalizationTypeFileRepository.cs
// Author:  Lana
// Created: 04 May 2020 13:50:37
// Purpose: Definition of Class HospitalizationTypeFileRepository

using Model.CustomExceptions;
using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Utilities;
using Repository.Generics;
using Repository.HospitalResourcesRepository;
using System;
using System.Collections.Generic;

namespace Repository.ScheduleRepository.HospitalizationsRepository
{
    public class HospitalizationTypeFileRepository : GenericFileRepository<HospitalizationType, int>, HospitalizationTypeRepository
    {
        private EquipmentTypeRepository equipmentTypeRepository;
        private DepartmentRepository departmentRepository;
        private IntegerKeyGenerator keyGenerator;

        public HospitalizationTypeFileRepository(EquipmentTypeRepository equipmentTypeRepository,
            DepartmentRepository departmentRepository, String filePath) : base(filePath)
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
                List<Department> appropriateDepartments = new List<Department>();
                foreach (Department department in entity.AppropriateDepartments)
                    appropriateDepartments.Add(departmentRepository.GetByID(department.GetKey()));
                entity.AppropriateDepartments = appropriateDepartments;
                List<EquipmentType> necessaryEquipment = new List<EquipmentType>();
                foreach (EquipmentType type in entity.NecessaryEquipment)
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