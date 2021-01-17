using System;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Model
{
    public class LoggedUser
    {
        private LoginCredentials Credentials { get; set; }
        private static Role Role { get; set; }
        public static String Cookie { get; private set; }

        public LoggedUser(LoginCredentials loginCredentials, Role role, String cookie)
        {
            Credentials = loginCredentials;
            Role = role;
            Cookie = cookie;
        }

        public static Boolean RoleEquals(Role role)
        {
            return Role.Equals(role);
        }
    }
}
