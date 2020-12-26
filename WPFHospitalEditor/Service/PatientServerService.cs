using HealthcareBase.Model.Users.Patient;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class PatientServerService : IPatientServerService
    {
        public IEnumerable<Patient> GetAllPatients()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("Patient/getAllPatients", Method.GET);
            var response = client.Get<IEnumerable<Patient>>(request);
            return response.Data;
        }

        public IEnumerable<Patient> GetFilteredPatients(string name)
        {
            var patients = new List<Patient>();
            List<Patient> allPatients = GetAllPatients().ToList();
            if (string.IsNullOrEmpty(name)) return allPatients;
            foreach (Patient patient in allPatients)
            {
                if (CompareInput(patient, name))
                    patients.Add(patient);
            }
            return patients;
        }

        private bool CompareInput(Patient patient, string name)
        {
            if (patient.Person.Name.ToLower().Contains(name.ToLower()) || patient.Person.Surname.ToLower().Contains(name.ToLower()))
                return true;
            return false;
        }
    }
}
