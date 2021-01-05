using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class DoctorServerService : IDoctorServerService
    {
        public IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/departments/" + departmentId, Method.GET);
            var response = client.Get<IEnumerable<DoctorDto>>(request);
            return response.Data;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/" + doctorId, Method.GET);
            var response = client.Get<Doctor>(request);
            return response.Data;
        }

        public IEnumerable<DoctorDto> GetAllSpecialists()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/specialists", Method.GET);
            var response = client.Get<IEnumerable<DoctorDto>>(request);
            return response.Data;
        }

        public IEnumerable<DoctorDto> SearchDoctors(string name)
        {
            List<DoctorDto> allDoctors = GetDoctorsByDepartment(AllConstants.RegularExaminationDepartment).ToList();
            return FilterDoctors(allDoctors, name);
        }

        public IEnumerable<DoctorDto> SearchSpecialists(string name)
        {
            List<DoctorDto> allDoctors = GetAllSpecialists().ToList();
            return FilterDoctors(allDoctors, name);
        }

        private bool CompareInput(DoctorDto doctorDto, string name)
        {
            if (doctorDto.Name.ToLower().Contains(name.ToLower()) || doctorDto.Surname.ToLower().Contains(name.ToLower())) 
                return true;
            return false;
        }

        private List<DoctorDto> FilterDoctors(List<DoctorDto> allDoctors, string name)
        {
            var doctors = new List<DoctorDto>();
            if (string.IsNullOrEmpty(name)) return allDoctors;
            foreach (DoctorDto doctorDto in allDoctors)
            {
                if (CompareInput(doctorDto, name))
                    doctors.Add(doctorDto);
            }
            return doctors;
        }
    }
}
