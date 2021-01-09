using System.Collections.Generic;
using User.API.DTOs;
using Doctor = User.API.Model.Users.Employees.Doctors.Doctor;

namespace User.API.Services.EmployeeService
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
        IEnumerable<DoctorDTO> GetDoctorsByDepartment(int departmentId);
        IEnumerable<DoctorDTO> GetAllSpecialists();
    }
}