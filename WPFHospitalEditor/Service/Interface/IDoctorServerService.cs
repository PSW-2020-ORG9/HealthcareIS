using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

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
