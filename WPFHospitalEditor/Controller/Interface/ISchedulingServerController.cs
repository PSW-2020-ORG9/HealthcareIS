using DesktopDTO;
using System.Collections.Generic;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface ISchedulingServerController
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recommendationRequestDto);
    }
}
