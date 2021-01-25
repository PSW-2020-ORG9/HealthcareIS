using Schedule.API.Model.Utilities;

namespace Schedule.API.DTOs
{
    public class SchedulingDto
    {
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
