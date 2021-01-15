using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Model
{
    public class LoggedUser
    {
        private LoginCredentials Credentials { get; set; }
        public static Role Role { get; private set; }
        public static String Cookie { get; private set; }

        public LoggedUser(LoginCredentials loginCredentials, Role role, String cookie)
        {
            Credentials = loginCredentials;
            Role = role;
            Cookie = cookie;
        }
    }
}
