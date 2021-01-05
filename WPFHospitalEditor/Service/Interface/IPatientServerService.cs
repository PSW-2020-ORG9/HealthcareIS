using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IPatientServerService
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> SearchPatients(string name);
    }
}
