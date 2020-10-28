// File:    CurrentScheduleContext.cs
// Author:  Lana
// Created: 02 June 2020 09:15:36
// Purpose: Definition of Class CurrentScheduleContext

using Service.HospitalResourcesService.RoomService;
using Service.ScheduleService.ProcedureService;
using Service.UsersService.EmployeeService;
using System;

namespace Service.ScheduleService
{
    public class CurrentScheduleContext
    {
        private Service.ScheduleService.ProcedureService.ProcedureService procedureService;
        private Service.ScheduleService.HospitalizationService.HospitalizationService hospitalizationService;
        private RenovationService renovationService;
        private ShiftService shiftService;

        public ProcedureService.ProcedureService ProcedureService { get => procedureService; set => procedureService = value; }
        public HospitalizationService.HospitalizationService HospitalizationService { get => hospitalizationService; set => hospitalizationService = value; }
        public RenovationService RenovationService { get => renovationService; set => renovationService = value; }
        public ShiftService ShiftService { get => shiftService; set => shiftService = value; }
    }
}