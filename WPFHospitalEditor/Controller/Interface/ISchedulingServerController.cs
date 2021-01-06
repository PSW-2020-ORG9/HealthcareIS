using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface ISchedulingServerController
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recommendationRequestDto);
    }
}
