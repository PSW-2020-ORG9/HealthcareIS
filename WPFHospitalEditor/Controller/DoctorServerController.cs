using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class DoctorServerController : IDoctorServerController
    {
        private readonly IDoctorServerService doctorServerService = new DoctorServerService();

        public IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId)
        {
            return doctorServerService.GetDoctorsByDepartment(departmentId);
        }
        public Doctor GetDoctorById(int doctorId)
        {
            return doctorServerService.GetDoctorById(doctorId);
        }
        public IEnumerable<DoctorDto> ShowFilteredDoctors(string name)
        {
            return doctorServerService.ShowFilteredDoctors(name);
        }

    }
}
