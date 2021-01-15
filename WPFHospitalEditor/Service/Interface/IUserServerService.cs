using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IUserServerService
    {
        string Login(LoginCredentials loginCredentials);
    }
}
