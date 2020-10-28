// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using Model.Users.Employee;
using Model.Users.UserAccounts;
using Repository.Generics;

namespace Model.Users.UserFeedback
{
    public class PatientSurveyResponse : Entity<int>
    {
        public int ExperienceRating { get; set; }

        public Doctor BestDoctor { get; set; }

        public PatientAccount Patient { get; set; }

        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is PatientSurveyResponse response &&
                   Id == response.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}