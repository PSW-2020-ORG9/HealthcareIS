// File:    PatientSurveyResponse.cs
// Author:  Lana
// Created: 21 April 2020 18:23:22
// Purpose: Definition of Class PatientSurveyResponse

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Model.Users.Survey
{
    public class Survey : IEntity<int>
    {
        [Key]public int Id { get; set; }
        public List<SurveySection> SurveySections { get; set; }
        
        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}