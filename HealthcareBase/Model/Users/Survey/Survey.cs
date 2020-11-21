// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class Survey : Entity<int>
    {
        
        public List<SurveySection> SurveySections { get; set; }
        [Key]public int Id { get; set; }

        public int GetKey() => Id;

        public void SetKey(int id) => Id = id;

        public override bool Equals(object obj)
        {
            return obj is Survey response &&
                   Id == response.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}