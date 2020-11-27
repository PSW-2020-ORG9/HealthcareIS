// File:    RecommendationRequestDTO.cs
// Author:  Lana
// Created: 02 June 2020 02:15:17
// Purpose: Definition of Class RecommendationRequestDTO

using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public class RecommendationRequestDTO
    {
        public Doctor Doctor { get; set; }

        public TimeInterval TimeInterval { get; set; }

        public Patient Patient { get; set; }
    }
}