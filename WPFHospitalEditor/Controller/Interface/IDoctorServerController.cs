using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IDoctorServerController
    {
        IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId);
        Doctor GetDoctorById(int doctorId);
        IEnumerable<DoctorDto> ShowFilteredDoctors(string name);
    }
}
