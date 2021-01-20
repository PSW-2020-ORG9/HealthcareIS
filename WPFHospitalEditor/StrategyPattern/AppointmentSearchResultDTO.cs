using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.StrategyPattern
{
    class AppointmentSearchResultDTO : SearchResultDTO
    {
        public RecommendationDto RecommendationDto { get; internal set; }
    }
}
