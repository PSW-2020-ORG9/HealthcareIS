using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IDoctorServerService
    {
        IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId);
        Doctor GetDoctorById(int doctorId);

        IEnumerable<DoctorDto> SearchDoctors(string name);
        IEnumerable<DoctorDto> GetAllSpecialists();
        IEnumerable<DoctorDto> SearchSpecialists(string name);
    }
}
