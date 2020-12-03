// File:    MedicationService.cs
// Author:  Korisnik
// Created: 25 May 2020 13:42:22
// Purpose: Definition of Class MedicationService

using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Dto;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.MedicationRepository.Interface;
using HealthcareBase.Service.MedicationService.Interface;

namespace HealthcareBase.Service.MedicationService
{
    public class MedicationService : IMedicationService
    {
        private readonly RepositoryWrapper<IMedicationRepository> medicationRepository;

        public MedicationService(IMedicationRepository medicationRepository)
        {
            this.medicationRepository = new RepositoryWrapper<IMedicationRepository>(medicationRepository);
        }

        private IEnumerable<MedicationDto> GetAll()
        {
            return medicationRepository.Repository.GetColumnsForMatching(
               condition: medication => medication.Id != 0,
               selection: medication => new MedicationDto()
               {
                   Id = medication.Id,
                   Description = medication.Description,
                   Name = medication.Name,
                   Quantity = 0,
                   Type = medication.Type
               }
           );
        }
        public IEnumerable<MedicationDto> GetAllMedicationsWithQuantity()
        {
            Dictionary<string, MedicationDto> allMedication = new Dictionary<string, MedicationDto>();
            foreach (MedicationDto medication in GetAll())
            {
                if (!allMedication.ContainsKey(medication.Name))
                {
                    allMedication[medication.Name] = medication;

                }
                allMedication[medication.Name].Quantity += 1;
            }
            return allMedication.Values.ToList();
        }
    }
}