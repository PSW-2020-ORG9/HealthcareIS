using HealthcareBase.Model.Users.Patient;
using RestSharp;
using System.Collections.Generic;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class PatientServerService : IPatientServerService
    {
        public IEnumerable<Patient> GetAllPatients()
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Patient/getAllPatients", Method.GET);
            var response = client.Get<IEnumerable<Patient>>(request);
            return response.Data;
        }
    }
}
