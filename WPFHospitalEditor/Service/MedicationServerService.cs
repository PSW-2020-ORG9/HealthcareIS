using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace WPFHospitalEditor.Service
{
   public class MedicationServerService : IMedicationServerService
    {
        public IEnumerable<MedicationDto> GetAllMedication()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("medication", Method.GET);
            var response = client.Get<IEnumerable<MedicationDto>>(request);
            return response.Data;
        }

        public IEnumerable<MedicationDto> GetAllMedicationByName(string name)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("medication/" + name, Method.GET);
            var response = client.Get<IEnumerable<MedicationDto>>(request);
            return response.Data;
        }

        public IEnumerable<MedicationDto> SearchMedications(string name)
        {
            var medications = new List<MedicationDto>();
            List<MedicationDto> allMedications = GetAllMedication().ToList();
            if (string.IsNullOrEmpty(name)) return allMedications;
            foreach (MedicationDto medicationDto in allMedications)
            {
                if (CompareInput(medicationDto, name))
                    medications.Add(medicationDto);
            }
            return medications;
        }

        private bool CompareInput(MedicationDto medicationDto, string name)
        {
            return medicationDto.Name.ToLower().Contains(name.ToLower());
        }
    }
}
