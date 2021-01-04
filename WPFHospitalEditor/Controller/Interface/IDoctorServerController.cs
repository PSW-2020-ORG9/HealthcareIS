using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IDoctorServerController
    {
        IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId);
        Doctor GetDoctorById(int doctorId);
        IEnumerable<DoctorDto> SearchDoctors(string name);
        IEnumerable<DoctorDto> GetAllSpecialists();
        IEnumerable<DoctorDto> SearchSpecialists(string name);
    }
}
