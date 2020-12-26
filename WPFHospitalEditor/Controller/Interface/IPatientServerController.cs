using HealthcareBase.Model.Users.Patient;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IPatientServerController
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> GetFilteredPatients(string name);
    }
}
