using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
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

        public IEnumerable<DoctorDto> SearchDoctors(string name)
        {
            return doctorServerService.SearchDoctors(name);
        }

        public IEnumerable<DoctorDto> GetAllSpecialists()
        {
            return doctorServerService.GetAllSpecialists();
        }

        public IEnumerable<DoctorDto> SearchSpecialists(string name)
        {
            return doctorServerService.SearchSpecialists(name);
        }

        public IEnumerable<int> GetDoctorsByRoomsAndShifts(SchedulingDto dto)
        {
            return doctorServerService.GetDoctorsByRoomsAndShifts(dto);
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialty(int specialtyId)
        {
            return doctorServerService.GetDoctorsBySpecialty(specialtyId);
        }
    }
}
