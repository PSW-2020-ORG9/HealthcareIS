using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class SchedulingServerController : ISchedulingServerController
    {
        private readonly ISchedulingServerService schedulingServerService = new SchedulingServerService();
        public List<RecommendationDto> GetAppointments(RecommendationRequestDto recommendationRequestDto)
        {
            return schedulingServerService.GetAppointments(recommendationRequestDto);
        }

        public List<EquipmentRelocationDto> GetEquipmentRelocationAppointments(SchedulingDto scheduleDto)
        {
            return schedulingServerService.GetEquipmentRelocationAppointments(scheduleDto);
        }
        public List<RecommendationDto> GetEmergencyAppointments(RecommendationRequestDto recommendationRequestDto)
        {
            return schedulingServerService.GetEmergencyAppointments(recommendationRequestDto);
        }

        public List<RenovationDto> GetRenovationAppointments(SchedulingDto schedulingDto)
        {
            return schedulingServerService.GetRenovationAppointments(schedulingDto);
        }
    }
}
