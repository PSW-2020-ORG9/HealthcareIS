namespace DesktopDTO
{
   public class MedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MedicineType Type { get; set; }
        public int Quantity { get; set; }
    }
}
