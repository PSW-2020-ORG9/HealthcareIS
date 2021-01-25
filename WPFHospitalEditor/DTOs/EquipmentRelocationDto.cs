using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class EquipmentRelocationDto
    {
        public string EquipmentType { get; set; }
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public int Amount { get; set; }
        public TimeInterval TimeInterval { get; set; }

        public SchedulingDto toSchedulingDto()
        {
            SchedulingDto schedulingDto = new SchedulingDto()
            {
                SourceRoomId = SourceRoomId,
                DestinationRoomId = DestinationRoomId,
                TimeInterval = this.TimeInterval

            };
            return schedulingDto;
        }
    }
}
