using DesktopDTO;
using System.Collections.Generic;

namespace WPFHospitalEditor.Service.Interface
{
    public interface ISchedulingServerService
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recDto);
    }
}
