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
    public class Patient : Entity<int>
    {
        public Patient() {}

        private int _nesto;

        public void createEmptyMedicalHistory()
            => this.MedicalHistory = new MedicalHistory.MedicalHistory();
        
        [Key]
        public int MedicalRecordID { get; set; }
        public string InsuranceNumber { get; set; }

        [ForeignKey("Person")]
        public string PersonJmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public PatientStatus Status { get; set; }

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