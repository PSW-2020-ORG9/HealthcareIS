using System.Collections.Generic;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Dependencies
{
    public class Doctor
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public IEnumerable<DoctorSpecialty> Specialties { get; set; }
        public Department Department { get; set; }
    }

    public class DoctorSpecialty
    {
        public int SpecialtyId { get; set; }
    }
}