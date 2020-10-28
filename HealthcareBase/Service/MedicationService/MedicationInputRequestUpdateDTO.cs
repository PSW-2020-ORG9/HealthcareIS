// File:    MedicationInputRequestUpdateDTO.cs
// Author:  Lana
// Created: 02 June 2020 12:29:54
// Purpose: Definition of Class MedicationInputRequestUpdateDTO

using Model.Requests;
using Model.Users.UserAccounts;
using System;

namespace Service.MedicationService
{
    public class MedicationInputRequestUpdateDTO
    {
        private Model.Requests.MedicationInputRequest inputRequest;
        private Model.Users.UserAccounts.EmployeeAccount reviewer;
        private String comment;

        public MedicationInputRequest InputRequest { get => inputRequest; set => inputRequest = value; }
        public EmployeeAccount Reviewer { get => reviewer; set => reviewer = value; }
        public string Comment { get => comment; set => comment = value; }

    }
}