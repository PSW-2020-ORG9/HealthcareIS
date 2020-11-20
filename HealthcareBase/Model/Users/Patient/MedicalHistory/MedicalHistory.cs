// File:    MedicalHistory.cs
// Author:  Gudli
// Created: 20 April 2020 21:21:26
// Purpose: Definition of Class MedicalHistory

using Microsoft.EntityFrameworkCore;
using Repository.Generics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient.MedicalHistory
{
    public class MedicalHistory : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public List<AllergyManifestation> Allergies { get; set; }

        public MedicalHistory()
        {
            PersonalHistory = new PersonalHistory();
            FamilyHistory = new FamilyHistory();
        }

        [ForeignKey("PersonalHistory")]
        public int PersonalHistoryId { get; set; }
        public PersonalHistory PersonalHistory { get; set; }

        [ForeignKey("FamilyHistoryId")]
        public int FamilyHistoryId { get; set; }
        public FamilyHistory FamilyHistory { get; set; }

        public int GetKey()
            => Id;

        public void SetKey(int id)
            => Id = id;
    }
}