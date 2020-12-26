

namespace User.API.Model.Users.Employees.Doctors
{
    public class DoctorSpecialty
    {
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}