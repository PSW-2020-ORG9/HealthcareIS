using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
   public class MedicationServerService : IMedicationServerService
    {
        public IEnumerable<MedicationDto> GetAllMedication()
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Medication/getAll", Method.GET);
            var response = client.Get<IEnumerable<MedicationDto>>(request);
            return response.Data;
        }

        public IEnumerable<MedicationDto> GetAllMedicationByName(string name)
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Medication/getByName/" + name, Method.GET);
            var response = client.Get<IEnumerable<MedicationDto>>(request);
            return response.Data;
        }
    }
}
