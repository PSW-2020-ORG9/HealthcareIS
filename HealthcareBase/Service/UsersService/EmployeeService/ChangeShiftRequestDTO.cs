// File:    ChangeShiftRequestDTO.cs
// Author:  Lana
// Created: 02 June 2020 14:29:22
// Purpose: Definition of Class ChangeShiftRequestDTO

using System.Collections.Generic;
using Model.Users.Employee;
using Model.Utilities;

namespace Service.UsersService.EmployeeService
{
    public class ChangeShiftRequestDTO
    {
        public List<Shift> Shifts { get; set; }

        public TimeInterval Time { get; set; }

        public Doctor Doctor { get; set; }
    }
}