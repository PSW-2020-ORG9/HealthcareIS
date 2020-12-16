using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HospitalWebApp.Dtos;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class SchedulingServerController : ISchedulingServerController
    {
        private ISchedulingServerService schedulingServerService = new SchedulingServerService();
        public List<RecommendationDto> getAppointments(RecommendationRequestDto recommendationRequestDto)
        {
            return schedulingServerService.getAppointments(recommendationRequestDto);
        }
    }
}
