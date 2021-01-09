using System.Collections.Generic;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IPatientServerService
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> SearchPatients(string name);
    }
}
