// File:    PersonalHistory.cs
// Author:  Gudli
// Created: 21 April 2020 15:36:43
// Purpose: Definition of Class PersonalHistory

using Microsoft.EntityFrameworkCore;
using Repository.Generics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Users.Patient.MedicalHistory
{
    public class PersonalHistory : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Overview { get; set; }
        public List<DiagnosisDetails> Diagnoses { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}