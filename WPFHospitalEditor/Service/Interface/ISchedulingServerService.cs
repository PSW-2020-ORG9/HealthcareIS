using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HospitalWebApp.Dtos;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface ISchedulingServerService
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recDto);
    }
}
