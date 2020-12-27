using System.Collections.Generic;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Dependencies
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public IEnumerable<Specialty> Specialties { get; set; }
    }
}