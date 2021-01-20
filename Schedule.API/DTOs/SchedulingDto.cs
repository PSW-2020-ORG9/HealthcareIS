using Schedule.API.Model.Utilities;

namespace Schedule.API.DTOs
{
    public class SchedulingDto
    {
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
