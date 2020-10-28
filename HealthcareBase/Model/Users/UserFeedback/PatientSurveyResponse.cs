// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.Generics;
using System;

namespace Model.Users.UserFeedback
{
    public class PatientSurveyResponse : Entity<int>
    {
        private int experienceRating;
        private Doctor bestDoctor;
        private PatientAccount patient;
        private int id;

        public int ExperienceRating { get => experienceRating; set => experienceRating = value; }
        public Doctor BestDoctor { get => bestDoctor; set => bestDoctor = value; }
        public PatientAccount Patient { get => patient; set => patient = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is PatientSurveyResponse response &&
                   id == response.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}