// File:    RecommendationRequestDTO.cs
// Author:  Lana
// Created: 02 June 2020 02:15:17
// Purpose: Definition of Class RecommendationRequestDTO

using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class RecommendationRequestDTO
    {
        public Doctor Doctor { get; set; }

        public TimeInterval TimeInterval { get; set; }

        public Patient Patient { get; set; }
    }
}