using System.ComponentModel.DataAnnotations.Schema;
using User.API.Model.Users.Employees.Doctors;

namespace User.API.Model.Users.UserAccounts
{
    public class DoctorAccount : UserAccount
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}