using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class RecommendationDto
    {
        public Doctor Doctor { get; internal set; }
        public TimeInterval TimeInterval { get; internal set; }
        public int RoomId { get; internal set; }
    }
}
