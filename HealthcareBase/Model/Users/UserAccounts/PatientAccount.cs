// File:    PatientAccount.cs
// Author:  Lana
// Created: 21 April 2020 11:34:46
// Purpose: Definition of Class PatientAccount

using Model.Users.Employee;
using System;
using System.Collections.Generic;
using Model.Users.Patient;

namespace Model.Users.UserAccounts
{
    public class PatientAccount : UserAccount
    {
        private Boolean respondedToSurvey;
        private Patient.Patient patient;
        private List<Doctor> favouriteDoctors;

        public bool RespondedToSurvey { get => respondedToSurvey; set => respondedToSurvey = value; }
        public Patient.Patient Patient { get => patient; set => patient = value; }

        public IEnumerable<Doctor> FavouriteDoctors
        {
            get
            {
                if (favouriteDoctors == null)
                    favouriteDoctors = new List<Doctor>();
                return favouriteDoctors;
            }
            set
            {
                RemoveAllFavouriteDoctors();
                if (value != null)
                {
                    foreach (Doctor oDoctor in value)
                        AddFavouriteDoctor(oDoctor);
                }
            }
        }

        public void AddFavouriteDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (favouriteDoctors == null)
                favouriteDoctors = new List<Doctor>();
            if (!favouriteDoctors.Contains(newDoctor))
                favouriteDoctors.Add(newDoctor);
        }

        public void RemoveFavouriteDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (favouriteDoctors != null)
                if (favouriteDoctors.Contains(oldDoctor))
                    favouriteDoctors.Remove(oldDoctor);
        }

        public void RemoveAllFavouriteDoctors()
        {
            if (favouriteDoctors != null)
                favouriteDoctors.Clear();
        }

        public override bool Equals(object obj)
        {
            return obj is PatientAccount account &&
                   id == account.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}