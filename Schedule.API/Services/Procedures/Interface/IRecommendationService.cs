﻿using System.Collections.Generic;
using Schedule.API.DTOs;
using Schedule.API.Model.Recommendations;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IRecommendationService
    {
        IEnumerable<RecommendationDto> Recommend(RecommendationRequestDto dto);
        IEnumerable<EquipmentRelocationDto> RecommendEquipmentRelocation(EquipmentRecommendationRequestDto dto);
        IEnumerable<RecommendationDto> RecommendEmergency(RecommendationRequestDto dto);
    }
}