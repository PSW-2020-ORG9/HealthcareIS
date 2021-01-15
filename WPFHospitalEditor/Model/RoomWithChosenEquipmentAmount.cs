namespace WPFHospitalEditor.Model
{
    public class RoomWithChosenEquipmentAmount
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int EquipmentAmount { get; set; }

        public RoomWithChosenEquipmentAmount(int id, string name, int amount)
        {
            this.RoomId = id;
            this.RoomName = name;
            this.EquipmentAmount = amount;
        }
    }
}
