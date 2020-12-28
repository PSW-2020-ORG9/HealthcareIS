namespace HealthcareBase.Dto
{
    public class EquipmentRealocationDto
    {
        public string EquipmentType { get; set; }
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public int Amount { get; set; }
    }
}
