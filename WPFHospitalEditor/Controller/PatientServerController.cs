using HealthcareBase.Model.Users.Patient;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class PatientServerController : IPatientServerController
    {
        private readonly IPatientServerService patientServerService = new PatientServerService();
        public IEnumerable<Patient> GetAllPatients()
        {
            return patientServerService.GetAllPatients();
        }
    }
}
