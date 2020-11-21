// File:    RequestStatusUpdateDTO.cs
// Author:  Lana
// Created: 02 June 2020 01:41:59
// Purpose: Definition of Class RequestStatusUpdateDTO

using HealthcareBase.Model.Requests;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Service.ScheduleService.ScheduleAdjustmentRequestService
{
    public class RequestStatusUpdateDTO
    {
        public ScheduleAdjustmentRequest Request { get; set; }

        public string Comment { get; set; }

        public EmployeeAccount Reviewer { get; set; }
    }
}