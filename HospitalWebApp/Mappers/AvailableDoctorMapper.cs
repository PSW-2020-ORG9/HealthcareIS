using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HealthcareBase.Model.Users.Employee.Doctors;

namespace HospitalWebApp.Mappers
{
    public class AvailableDoctorMapper
    {
        public  static List<AvailableDoctorDTO> ObjectToDto(IEnumerable<Doctor> doctors)
        {
           return doctors.Select(doctor =>
               new AvailableDoctorDTO
               {
                   DepartmentName = doctor.Department.Name,
                   DoctorId = doctor.Id,
                   Name = doctor.Person.Name,
                   Surname = doctor.Person.Surname
               }).ToList();
        }
    }
}