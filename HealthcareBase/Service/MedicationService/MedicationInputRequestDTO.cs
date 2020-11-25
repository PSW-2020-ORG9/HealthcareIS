using System.Collections.Generic;
using Model.Medication;
using Model.Users.Employee;
using Model.Users.UserAccounts;

namespace Service.MedicationService
{
    public class MedicationInputRequestDTO
    {
        public Medication Medication { get; set; }

        public DoctorAccount Sender { get; set; }

        public List<Specialty> Specialties { get; set; }
    }
}