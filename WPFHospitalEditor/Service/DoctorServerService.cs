﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service
{
    public class DoctorServerService : IDoctorServerService
    {
        public IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/departments/" + departmentId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<DoctorDto>>(request);
            return response.Data;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/" + doctorId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<Doctor>(request);
            return response.Data;
        }

        public IEnumerable<DoctorDto> GetAllSpecialists()
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/specialists", Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
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

        public IEnumerable<int> GetDoctorsByRoomsAndShifts(SchedulingDto dto)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/schedule/examination/get-doctors-by-rooms-and-shifts", Method.POST);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            request.AddJsonBody(EquipmentRelocationDtoToJson(dto));
            var response = client.Post<List<int>>(request);
            return response.Data;
        }

        private String EquipmentRelocationDtoToJson(SchedulingDto dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialty(int specialtyId)
        {
            var client = new RestClient(AllConstants.ConnectionUrl);
            var request = new RestRequest("/api/user/doctor/specialty/" + specialtyId, Method.GET);
            request.AddParameter(AllConstants.AuthorizationTokenKey, LoggedUser.Cookie, ParameterType.Cookie);
            var response = client.Get<IEnumerable<Doctor>>(request);
            return response.Data;
        }
    }
}
