using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class SpecialtyServerService : ISpecialtyServerService
    {
        public IEnumerable<Specialty> GetAllSpecialties()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/specialty", Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<Specialty>>(request);
            return response.Data;
        }
    }
}
