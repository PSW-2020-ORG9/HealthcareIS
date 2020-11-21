using System.Collections.Generic;
using HealthcareBase.Model.Medication;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Service.MedicationService
{
    public class MedicationInputRequestDTO
    {
        public Medication Medication { get; set; }

        public EmployeeAccount Sender { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}