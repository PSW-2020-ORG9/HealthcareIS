// File:    FamilyHistory.cs
// Author:  Gudli
// Created: 21 April 2020 15:19:06
// Purpose: Definition of Class FamilyHistory

using Microsoft.EntityFrameworkCore;
using Repository.Generics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Users.Patient.MedicalHistory
{
    public class FamilyHistory : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Overview { get; set; }
        public List<FamilyMemberDiagnosis> Diagnoses { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}