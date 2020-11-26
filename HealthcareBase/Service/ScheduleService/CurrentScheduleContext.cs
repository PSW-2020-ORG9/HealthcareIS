// File:    CurrentScheduleContext.cs
// Author:  Lana
// Created: 02 June 2020 09:15:36
// Purpose: Definition of Class CurrentScheduleContext

using HealthcareBase.Service.HospitalResourcesService.RoomService;
using HealthcareBase.Service.UsersService.EmployeeService;

namespace HealthcareBase.Service.ScheduleService
{
    public class CurrentScheduleContext
    {
        public ProcedureService.ProcedureService ProcedureService { get; set; }

        public HospitalizationService.HospitalizationService HospitalizationService { get; set; }

        public RenovationService RenovationService { get; set; }

        public ShiftService ShiftService { get; set; }
    }
}