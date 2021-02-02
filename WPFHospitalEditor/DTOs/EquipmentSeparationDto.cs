using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.DTOs
{
    public class EquipmentSeparationDto
    {
        public int SourceRoomId { get; set; }
        public int SourceQuantity { get; set; }
        public int DestinationRoomId { get; set; }
        public int DestinationQuantity { get; set; }
        public string Name { get; set; }
    }
}
