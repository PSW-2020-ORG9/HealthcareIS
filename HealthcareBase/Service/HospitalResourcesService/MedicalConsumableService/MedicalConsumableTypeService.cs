// File:    MedicalConsumableTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:33:12
// Purpose: Definition of Class MedicalConsumableTypeService

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.HospitalResourcesRepository;

namespace Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableTypeService
    {
        private readonly MedicalConsumableRepository medicalConsumableRepository;
        private readonly MedicalConsumableTypeRepository medicalConsumableTypeRepository;

        public MedicalConsumableTypeService(MedicalConsumableTypeRepository medicalConsumableTypeRepository,
            MedicalConsumableRepository medicalConsumableRepository)
        {
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            this.medicalConsumableRepository = medicalConsumableRepository;
        }

        public MedicalConsumableType GetByID(int id)
        {
            return medicalConsumableTypeRepository.GetByID(id);
        }

        public IEnumerable<MedicalConsumableType> GetAll()
        {
            return medicalConsumableTypeRepository.GetAll();
        }

        public MedicalConsumableType Create(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Create(medicalConsumableType);
        }

        public MedicalConsumableType Update(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Update(medicalConsumableType);
        }

        public void Delete(MedicalConsumableType medicalConsumableType)
        {
            if (medicalConsumableRepository.ExistsByType(medicalConsumableType))
                throw new BadRequestException();
            medicalConsumableTypeRepository.Delete(medicalConsumableType);
        }
    }
}