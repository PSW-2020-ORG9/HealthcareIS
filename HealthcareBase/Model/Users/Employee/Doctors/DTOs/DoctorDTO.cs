namespace HealthcareBase.Model.Users.Employee.Doctors.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecialtyId { get; set; }
        public string DepartmentName { get; set; }
    }
}