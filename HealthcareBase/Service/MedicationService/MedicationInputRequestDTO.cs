using System.Collections.Generic;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Service.MedicationService
{
    public class MedicationInputRequestDTO
    {
        public Medication Medication { get; set; }

        public DoctorAccount Sender { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}