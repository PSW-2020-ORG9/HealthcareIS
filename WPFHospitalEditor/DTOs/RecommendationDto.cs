using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class RecommendationDto
    {
        public Doctor Doctor { get; set; }
        public TimeInterval TimeInterval { get;  set; }
        public int RoomId { get; set; }
    }
}
