// File:    MedicationInputRequestUpdateDTO.cs
// Author:  Lana
// Created: 02 June 2020 12:29:54
// Purpose: Definition of Class MedicationInputRequestUpdateDTO

using Model.Requests;
using Model.Users.UserAccounts;

namespace Service.MedicationService
{
    public class MedicationInputRequestUpdateDTO
    {
        public MedicationInputRequest InputRequest { get; set; }

        public EmployeeAccount Reviewer { get; set; }

        public string Comment { get; set; }
    }
}