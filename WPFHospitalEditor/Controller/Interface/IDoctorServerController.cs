using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IDoctorServerController
    {
        IEnumerable<DoctorDto> GetDepartmentDoctors(int departmentId);
        Doctor getDoctorById(int doctorId);
    }
}
