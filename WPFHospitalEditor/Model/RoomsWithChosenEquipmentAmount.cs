namespace WPFHospitalEditor.Model
{
    public class RoomsWithChosenEquipmentAmount
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int EquipmentAmount { get; set; }

        public RoomsWithChosenEquipmentAmount(int id, string name, int amount)
        {
            this.RoomId = id;
            this.RoomName = name;
            this.EquipmentAmount = amount;
        }
    }
}
