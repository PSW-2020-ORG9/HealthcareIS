using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class UserServerController : IUserServerController
    {
        private readonly IUserServerService userServerService = new UserServerService();
        public string Login(LoginCredentials loginCredentials)
        {
            return userServerService.Login(loginCredentials);
        }
    }
}
