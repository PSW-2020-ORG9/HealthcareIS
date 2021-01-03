using System.Collections.Generic;
using System.Linq;
using Schedule.API.DTOs;
using Schedule.API.Model.Dependencies;

namespace Schedule.API.Mappers
{
    public static class DoctorMapper
    {
        public static List<DoctorDTO> ObjectToDto(IEnumerable<Doctor> doctors)
        {
           return doctors.Select(doctor =>
               new DoctorDTO
               {
                   DoctorId = doctor.Id,
                   Name = doctor.Person.Name,
                   Surname = doctor.Person.Surname,
               }).ToList();
        }
    }
}