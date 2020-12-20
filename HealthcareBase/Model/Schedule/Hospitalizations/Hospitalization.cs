// File:    Hospitalization.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Class Hospitalization

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.HospitalResources;
using HealthcareBase.Model.Miscellaneous;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Schedule.Hospitalizations
{
    public class Hospitalization : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string CauseOfAdmission { get; set; }
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public DischargeType DischargeType { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public Room Room { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public HospitalizationType HospitalizationType { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}