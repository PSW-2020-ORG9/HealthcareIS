using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Model.Recommendations
{
    public class RecommendationDto
    {
        public Doctor Doctor { get; set; }
        public TimeInterval TimeInterval { get; set; }
        public int RoomId { get; set; }
    }
}