namespace WPFHospitalEditor.DTOs
{
    public class EquipmentRelocationDto
    {
        public string EquipmentType { get; set; }
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public int Amount { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
