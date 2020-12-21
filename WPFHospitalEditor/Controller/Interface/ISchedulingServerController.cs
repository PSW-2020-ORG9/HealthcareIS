using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HospitalWebApp.Dtos;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface ISchedulingServerController
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recommendationRequestDto);
    }
}
