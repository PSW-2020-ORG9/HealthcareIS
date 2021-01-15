using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IUserServerController
    {
        string Login(LoginCredentials loginCredentials);
    }
}
