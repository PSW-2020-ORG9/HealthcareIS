// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Patient
{
    public class Patient : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string InsuranceNumber { get; set; }

        [ForeignKey("Person")]
        public string Jmbg { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public PatientStatus Status { get; set; }

        [ForeignKey("MedicalRecord")]
        public int MedicalRecordId { get; set; }
        public MedicalHistory.MedicalRecord MedicalRecord { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}