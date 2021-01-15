using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;

namespace WPFHospitalEditor.DTOs
{
    public class LoginCredentials
    {
        private string Email { get; set; }
        private string Password { get; set; }
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
