using DesktopDTO;
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
