// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using Model.Users.Generalities;
using Repository.Generics;

namespace Model.Users.Patient
{
    public class Patient : Person, Entity<int>
    {
        public Patient()
        {
            MedicalHistory = new MedicalHistory.MedicalHistory();
        }

        public string InsuranceNumber { get; set; }

        public string MiddleName { get; set; }

        public MaritalStatus MartialStatus { get; set; }

        public PatientStatus Status { get; set; }

        public City CityOfBirth { get; set; }

        public int MedicalRecordID { get; set; }

        public MedicalHistory.MedicalHistory MedicalHistory { get; set; }

        public int GetKey()
        {
            return MedicalRecordID;
        }

        public void SetKey(int id)
        {
            MedicalRecordID = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   MedicalRecordID == patient.MedicalRecordID;
        }

        public override int GetHashCode()
        {
            return -98446404 + MedicalRecordID.GetHashCode();
        }
    }
}