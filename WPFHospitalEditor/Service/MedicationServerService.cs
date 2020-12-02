using HealthcareBase.Dto;
using RestSharp;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service
{
   public class MedicationServerService
    {
        public IEnumerable<MedicationDto> GetAllMedication()
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Medication/getAll", Method.GET);
            var response = client.Get<IEnumerable<MedicationDto>>(request);
            return response.Data;
        }
    }
}
