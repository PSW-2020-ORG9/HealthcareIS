using System.Collections.Generic;
using HealthcareBase.Model.Users.Employee;

namespace HealthcareBase.Service.UsersService.EmployeeService
{
    public interface IDoctorService
    {
        public IEnumerable<Doctor> GetAll();
        
    }
}