using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IPatientServerController
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> SearchPatients(string name);
    }
}
