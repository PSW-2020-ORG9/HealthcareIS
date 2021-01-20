using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class SchedulingDto
    {
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
