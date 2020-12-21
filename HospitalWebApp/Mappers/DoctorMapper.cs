using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;

namespace HospitalWebApp.Mappers
{
    public static class DoctorMapper
    {
        public static List<DoctorDto> ObjectToDto(IEnumerable<Doctor> doctors)
        {
           return doctors.Select(doctor =>
               new DoctorDto
               {
                   DoctorId = doctor.Id,
                   Name = doctor.Person.Name,
                   Surname = doctor.Person.Surname,
               }).ToList();
        }
    }
}