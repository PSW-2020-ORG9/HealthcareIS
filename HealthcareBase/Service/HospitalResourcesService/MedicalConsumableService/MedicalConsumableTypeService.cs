// File:    MedicalConsumableTypeService.cs
// Author:  Lana
// Created: 28 May 2020 11:33:12
// Purpose: Definition of Class MedicalConsumableTypeService

using System.Collections.Generic;
using Model.CustomExceptions;
using Model.HospitalResources;
using Repository.Generics;
using Repository.HospitalResourcesRepository;

namespace Service.HospitalResourcesService.MedicalConsumableService
{
    public class MedicalConsumableTypeService
    {
        private readonly RepositoryWrapper<MedicalConsumableRepository> medicalConsumableRepository;
        private readonly RepositoryWrapper<MedicalConsumableTypeRepository> medicalConsumableTypeRepository;

        public MedicalConsumableTypeService(
            RepositoryWrapper<MedicalConsumableTypeRepository> medicalConsumableTypeRepository,
            RepositoryWrapper<MedicalConsumableRepository> medicalConsumableRepository
            )
        {
            this.medicalConsumableTypeRepository = medicalConsumableTypeRepository;
            this.medicalConsumableRepository = medicalConsumableRepository;
        }

        public MedicalConsumableType GetByID(int id)
        {
            return medicalConsumableTypeRepository.Repository.GetByID(id);
        }

        public IEnumerable<MedicalConsumableType> GetAll()
        {
            return medicalConsumableTypeRepository.Repository.GetAll();
        }

        public MedicalConsumableType Create(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Repository.Create(medicalConsumableType);
        }

        public MedicalConsumableType Update(MedicalConsumableType medicalConsumableType)
        {
            return medicalConsumableTypeRepository.Repository.Update(medicalConsumableType);
        }

        public void Delete(MedicalConsumableType medicalConsumableType)
        {
            if (medicalConsumableRepository.Repository.ExistsByType(medicalConsumableType))
                throw new BadRequestException();
            medicalConsumableTypeRepository.Repository.Delete(medicalConsumableType);
        }
    }
}