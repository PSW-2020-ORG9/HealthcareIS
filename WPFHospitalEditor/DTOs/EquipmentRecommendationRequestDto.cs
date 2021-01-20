using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class EquipmentRecommendationRequestDto
    {
        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
