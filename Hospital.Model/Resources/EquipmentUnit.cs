using General;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.API.Model.Resources
{
    public class EquipmentUnit : Entity<int>
    {
        public DateTime AcquisitionDate { get; set; }
        public string Manufacturer { get; set; }

        [ForeignKey("CurrentLocation")]
        public int? CurrentLocationId { get; set; }
        public Room CurrentLocation { get; set; }

        [ForeignKey("EquipmentType")]
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}