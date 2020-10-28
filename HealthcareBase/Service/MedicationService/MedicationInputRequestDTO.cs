using Model.Medication;
using Model.Users.Employee;
using Model.Users.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MedicationService
{
    public class MedicationInputRequestDTO
    {
        Medication medication;
        EmployeeAccount sender;
        List<Specialty> specialties;

        public Medication Medication { get => medication; set => medication = value; }
        public EmployeeAccount Sender { get => sender; set => sender = value; }
        public List<Specialty> Specialties { get => specialties; set => specialties = value; }
    }
}
