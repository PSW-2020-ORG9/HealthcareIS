// File:    Patient.cs
// Author:  Lana
// Created: 13 April 2020 18:23:49
// Purpose: Definition of Class Patient

using Model.Users.Generalities;
using System;

namespace Model.Users.Patient
{
    public class Patient : Model.Users.Generalities.Person, Repository.Generics.Entity<int>
    {
        private String insuranceNumber;
        private String middleName;
        private MaritalStatus martialStatus;
        private PatientStatus status;
        private City cityOfBirth;
        private int medicalRecordID;
        private MedicalHistory.MedicalHistory medicalHistory;

        public Patient()
        {
            medicalHistory = new MedicalHistory.MedicalHistory();
        }

        public string InsuranceNumber { get => insuranceNumber; set => insuranceNumber = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public MaritalStatus MartialStatus { get => martialStatus; set => martialStatus = value; }
        public PatientStatus Status { get => status; set => status = value; }
        public City CityOfBirth { get => cityOfBirth; set => cityOfBirth = value; }

        public int MedicalRecordID { get => medicalRecordID; set => medicalRecordID = value; }
        public MedicalHistory.MedicalHistory MedicalHistory { get => medicalHistory; set => medicalHistory = value; }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   medicalRecordID == patient.medicalRecordID;
        }

        public override int GetHashCode()
        {
            return -98446404 + medicalRecordID.GetHashCode();
        }

        public int GetKey()
        {
            return medicalRecordID;
        }

        public void SetKey(int id)
        {
            medicalRecordID = id;
        }
    }
}