// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using Model.Users.Generalities;
using Repository.Generics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Users.Patient
{
    public class Patient : Person, Entity<int>
    {
        public Patient() {}

        public void createEmptyMedicalHistory()
            => this.MedicalHistory = new MedicalHistory.MedicalHistory();
        
        [Key]
        public int MedicalRecordID { get; set; }
        public string InsuranceNumber { get; set; }
        public string MiddleName { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public MaritalStatus MartialStatus { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public PatientStatus Status { get; set; }

        [ForeignKey("CityOfBirth")]
        public int CityOfBirthId { get; set; }
        public City CityOfBirth { get; set; }

        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }
        public MedicalHistory.MedicalHistory MedicalHistory { get; set; }

        public int GetKey() => MedicalRecordID;
        public void SetKey(int id) => MedicalRecordID = id;

        public override bool Equals(object obj)
            => obj is Patient patient &&
               MedicalRecordID == patient.MedicalRecordID;
    }
}