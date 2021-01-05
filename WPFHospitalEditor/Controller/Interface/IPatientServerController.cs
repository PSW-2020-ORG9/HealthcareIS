using System.Collections.Generic;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IPatientServerController
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> SearchPatients(string name);
    }
}
