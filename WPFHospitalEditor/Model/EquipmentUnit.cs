using System.ComponentModel.DataAnnotations.Schema;

namespace WPFHospitalEditor.Model
{
    public class EquipmentUnit : Entity<int>
    {

        [ForeignKey("CurrentLocation")]
        public int? CurrentLocationId { get; set; }
        public Room CurrentLocation { get; set; }

        [ForeignKey("EquipmentType")]
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}