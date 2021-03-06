﻿using System.Collections.Generic;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IDoctorServerController
    {
        IEnumerable<DoctorDto> GetDoctorsByDepartment(int departmentId);
        IEnumerable<Doctor> GetDoctorsBySpecialty(int specialtyId);
        Doctor GetDoctorById(int doctorId);
        IEnumerable<DoctorDto> SearchDoctors(string name);
        IEnumerable<DoctorDto> GetAllSpecialists();
        IEnumerable<DoctorDto> SearchSpecialists(string name);
        IEnumerable<int> GetDoctorsByRoomsAndShifts(SchedulingDto dto);
    }
}
