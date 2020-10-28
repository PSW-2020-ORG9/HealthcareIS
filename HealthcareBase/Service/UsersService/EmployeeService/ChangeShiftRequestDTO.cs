// File:    ChangeShiftRequestDTO.cs
// Author:  Lana
// Created: 02 June 2020 14:29:22
// Purpose: Definition of Class ChangeShiftRequestDTO

using Model.HospitalResources;
using Model.Users.Employee;
using Model.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Service.UsersService.EmployeeService
{
    public class ChangeShiftRequestDTO
    {
        private TimeInterval time;
        private Doctor doctor;
        private List<Shift> shifts;

        public List<Shift> Shifts { get => shifts; set => shifts = value; }
        public TimeInterval Time { get => time; set => time = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
    }
}