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

namespace Model.Users.Patient.MedicalHistory
{
    public class MedicalRecord : Entity<int>
    {
        [Key]
        public int Id { get; set; }

        public MedicalRecord() {}

        public IEnumerable<AllergyManifestation> Allergies { get; set; }
        public IEnumerable<ExaminationForPatient> Examinations { get; set; }
        public IEnumerable<SurgeryForPatient> Surgeries { get; set; }

        public IEnumerable<PersonalHistoryDiagnosis> PersonalHistoryDiagnoses { get; set; }
        public IEnumerable<FamilyMemberDiagnosis> FamilyMemberDiagnoses { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}