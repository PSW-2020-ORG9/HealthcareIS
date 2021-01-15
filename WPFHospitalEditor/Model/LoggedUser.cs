using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Model
{
    public class LoggedUser
    {
        public LoginCredentials Credentials { get; set; }
        public static Role Role { get; set; }
        public static String Cookie { get; set; }

        public LoggedUser(String email, String password)
        {
            Credentials = new LoginCredentials { Email = email, Password = password };
        }

    }
}
