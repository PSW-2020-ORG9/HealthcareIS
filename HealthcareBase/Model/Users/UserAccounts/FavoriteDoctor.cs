using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.Employee;

namespace Model.Users.UserAccounts
{
    public class FavoriteDoctor
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}