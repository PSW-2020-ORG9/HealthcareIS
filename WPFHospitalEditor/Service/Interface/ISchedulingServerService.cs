﻿using System.Collections.Generic;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service.Interface
{
    public interface ISchedulingServerService
    {
        List<RecommendationDto> GetAppointments(RecommendationRequestDto recDto);
        List<EquipmentRelocationDto> GetEquipmentRelocationAppointments(EquipmentRecommendationRequestDto equipmentRecommendationRequestDto);
        List<RecommendationDto> GetEmergencyAppointments(RecommendationRequestDto recDto);
    }
}
