using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class UserServerService : IUserServerService
    {
        public string Login(LoginCredentials loginCredentials)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("api/user/auth/login", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(loginCredentials));
            var response = client.Execute(request);
            if(!response.StatusCode.ToString().Equals("BadRequest"))
            { 
                return response.Cookies.First().Value;

            }
            return null;
        }
    }
}
