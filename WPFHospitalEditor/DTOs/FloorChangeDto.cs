using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.DTOs
{
    public class FloorChangeDto
    {
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
    }
}
