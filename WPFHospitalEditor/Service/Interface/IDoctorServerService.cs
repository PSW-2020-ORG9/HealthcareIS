using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IDoctorServerService
    {
        IEnumerable<DoctorDto> GetDepartmentDoctors(int departmentId);
        Doctor getDoctorById(int doctorId);
    }
}
