using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId);
    }
}