// File:    RecommendationRequestDTO.cs
// Author:  Lana
// Created: 02 June 2020 02:15:17
// Purpose: Definition of Class RecommendationRequestDTO

using Model.Users.Employee;
using Model.Users.Patient;
using Model.Utilities;
using System;

namespace Service.ScheduleService.PatientRecommendationService
{
    public class RecommendationRequestDTO
    {
        private Doctor doctor;
        private TimeInterval timeInterval;
        private Patient patient;

        public Doctor Doctor { get => doctor; set => doctor = value; }
        public TimeInterval TimeInterval { get => timeInterval; set => timeInterval = value; }
        public Patient Patient { get => patient; set => patient = value; }
    }
}