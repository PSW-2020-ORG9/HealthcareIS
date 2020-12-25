using System.ComponentModel.DataAnnotations.Schema;

namespace User.API.Model.Users.Employees.Doctors
{
    public class DoctorSpecialty
    {
        [ForeignKey("Specialty")]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}