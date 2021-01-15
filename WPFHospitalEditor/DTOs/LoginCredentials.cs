using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;

namespace WPFHospitalEditor.DTOs
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        IUserServerController userServerController = new UserServerController();

        public LoginCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Login(LoginCredentials loginCredentials)
        {
            String cookie = userServerController.Login(loginCredentials);
            if (cookie != null)
                return cookie;
            return null;
        }
    }
}
