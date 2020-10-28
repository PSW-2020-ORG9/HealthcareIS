// File:    RequestStatusUpdateDTO.cs
// Author:  Lana
// Created: 02 June 2020 01:41:59
// Purpose: Definition of Class RequestStatusUpdateDTO

using Model.Requests;
using Model.Users.UserAccounts;
using System;

namespace Service.ScheduleService.ScheduleAdjustmentRequestService
{
    public class RequestStatusUpdateDTO
    {
        private ScheduleAdjustmentRequest request;
        private String comment;
        private EmployeeAccount reviewer;

        public ScheduleAdjustmentRequest Request { get => request; set => request = value; }
        public string Comment { get => comment; set => comment = value; }
        public EmployeeAccount Reviewer { get => reviewer; set => reviewer = value; }
    }
}