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
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Doctor/getDoctorsByDepartment/" + departmentId, Method.GET);
            var response = client.Get<IEnumerable<DoctorDto>>(request);
            return response.Data;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            var client = new RestClient(AllConstants.connectionUrl);
            var request = new RestRequest("Doctor/getDoctorById/" + doctorId, Method.GET);
            var response = client.Get<Doctor>(request);
            return response.Data;
        }

        public IEnumerable<DoctorDto> GetFilteredDoctors(string name)
        {
            var doctors = new List<DoctorDto>();
            List<DoctorDto> allDoctors = GetDoctorsByDepartment(AllConstants.regularExaminationDepartment).ToList();
            if (string.IsNullOrEmpty(name)) return allDoctors;
            foreach (DoctorDto doctorDto in allDoctors)
            {
                if (CompareInput(doctorDto, name))
                    doctors.Add(doctorDto);
            }
            return doctors;
        }

        private bool CompareInput(DoctorDto doctorDto, string name)
        {
            return doctorDto.Name.ToLower().Contains(name.ToLower());
        }
    }
}
