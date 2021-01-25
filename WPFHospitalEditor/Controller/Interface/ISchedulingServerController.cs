using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface ISchedulingServerController
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recommendationRequestDto);
        List<EquipmentRelocationDto> GetEquipmentRelocationAppointments(SchedulingDto scheduleDto);
        List<RecommendationDto> GetEmergencyAppointments(RecommendationRequestDto recDto);
        List<RenovationDto> GetRenovationAppointments(SchedulingDto schedulingDto);
    }
}
