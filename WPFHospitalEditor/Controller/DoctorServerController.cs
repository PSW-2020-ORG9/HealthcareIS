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
        private IDoctorServerService doctorServerService = new DoctorServerService();

        public IEnumerable<DoctorDto> GetDepartmentDoctors(int departmentId)
        {
            return doctorServerService.GetDepartmentDoctors(departmentId);
        }
        public Doctor getDoctorById(int doctorId)
        {
            return doctorServerService.getDoctorById(doctorId);
        }
    }
}
