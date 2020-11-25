using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.Employee;

namespace Model.Users.UserAccounts
{
    public class DoctorAccount : UserAccount
    {
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}