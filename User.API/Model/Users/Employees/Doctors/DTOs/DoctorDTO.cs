namespace User.API.Model.Users.Employees.Doctors.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecialtyId { get; set; }
    }
}