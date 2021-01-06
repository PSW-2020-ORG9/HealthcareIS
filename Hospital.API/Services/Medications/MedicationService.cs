using General.Repository;
using Hospital.API.DTOs;
using Hospital.API.Infrastructure.Repositories.Medications;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.API.Services.Medications
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
        private IEnumerable<MedicationDto> GetAllByName(string name)
        {
            return medicationRepository.Repository.GetColumnsForMatching(
               condition: medication => medication.Name.Equals(name),
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

        public IEnumerable<MedicationDto> GetAllMedicationWithQuantityByName(string name)
        {
            Dictionary<string, MedicationDto> allMedication = new Dictionary<string, MedicationDto>();
            foreach (MedicationDto medication in GetAllByName(name))
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