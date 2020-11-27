using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee.Doctors;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAll();
    }
}