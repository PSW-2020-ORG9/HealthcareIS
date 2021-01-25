using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class EventStoreServerService : IEventStoreServerService
    {
        public string RecordEquipmentLookup(EquipmentLookupDto equipmentLookupDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/event/equipmentlookup", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(equipmentLookupDto));
            var response = client.Execute(request);
            return response.StatusCode.ToString();
        }

        public string RecordFloorChange(FloorChangeDto floorChangeDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/event/floorchange", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(floorChangeDto));
            var response = client.Execute(request);
            return response.StatusCode.ToString();
        }

        public string RecordMedicationLookup(MedicationLookupDto medicationLookupDto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/event/medicationlookup", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(JsonConvert.SerializeObject(medicationLookupDto));
            var response = client.Execute(request);
            return response.StatusCode.ToString();
        }
    }
}
