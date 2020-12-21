using HealthcareBase.Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IPatientServerService
    {
        IEnumerable<Patient> GetAllPatients();
    }
}
