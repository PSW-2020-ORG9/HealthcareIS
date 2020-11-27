using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;

namespace HealthcareBase.Model.Users.UserAccounts
{
    public class DoctorAccount : UserAccount
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}