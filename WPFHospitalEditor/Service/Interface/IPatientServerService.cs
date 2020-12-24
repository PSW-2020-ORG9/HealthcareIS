﻿using HealthcareBase.Model.Users.Patient;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IPatientServerService
    {
        IEnumerable<Patient> GetAllPatients();
    }
}