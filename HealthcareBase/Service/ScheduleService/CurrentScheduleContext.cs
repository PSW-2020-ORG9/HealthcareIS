// File:    CurrentScheduleContext.cs
// Author:  Lana
// Created: 02 June 2020 09:15:36
// Purpose: Definition of Class CurrentScheduleContext

using Service.HospitalResourcesService.RoomService;
using Service.UsersService.EmployeeService;

namespace Service.ScheduleService
{
    public class CurrentScheduleContext
    {
        public ProcedureService.ProcedureService ProcedureService { get; set; }

        public HospitalizationService.HospitalizationService HospitalizationService { get; set; }

        public RenovationService RenovationService { get; set; }

        public ShiftService ShiftService { get; set; }
    }
}