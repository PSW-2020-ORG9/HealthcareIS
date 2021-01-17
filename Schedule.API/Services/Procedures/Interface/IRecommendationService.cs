using System.Collections.Generic;
using Schedule.API.Model.Recommendations;

namespace Schedule.API.Services.Procedures.Interface
{
    public interface IRecommendationService
    {
        public IEnumerable<RecommendationDto> Recommend(RecommendationRequestDto dto);
        public IEnumerable<RecommendationDto> RecommendEmergency(RecommendationRequestDto dto);
    }
}