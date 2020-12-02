// File:    PrioritiseDoctorStrategy.cs
// Author:  Lana
// Created: 02 June 2020 02:58:44
// Purpose: Definition of Class PrioritiseDoctorStrategy

using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Service.ScheduleService.ScheduleFittingService;

namespace HealthcareBase.Service.ScheduleService.PatientRecommendationService
{
    public class PrioritiseDoctorStrategy : IRecommendationStrategy
    {
        public Examination ChooseBest(Examination examination)
        {
            throw new NotImplementedException();
        }
    }
}