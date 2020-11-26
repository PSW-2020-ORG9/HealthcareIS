using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Employee;

namespace HealthcareBase.Model.Users.UserAccounts
{
    public class FavoriteDoctor
    {
        public int PatientAccountId { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}