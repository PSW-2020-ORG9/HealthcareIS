// File:    MedicalHistory.cs
// Author:  Gudli
// Created: 20 April 2020 21:21:26
// Purpose: Definition of Class MedicalHistory

using Microsoft.EntityFrameworkCore;
using Repository.Generics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Schedule.Procedures;
using Model.Users.Patient.MedicalHistory.Relationship;

namespace Model.Users.Patient.MedicalHistory
{
    public class MedicalRecord : Entity<int>
    {
        [Key]
        public int Id { get; set; }

        public IEnumerable<AllergyManifestation> Allergies { get; set; }
        public IEnumerable<Examination> Examinations { get; set; }
        public IEnumerable<Surgery> Surgeries { get; set; }

        public IEnumerable<FamilyMemberDiagnosis> FamilyMemberDiagnoses { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}