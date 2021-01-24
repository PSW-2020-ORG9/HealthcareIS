using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.StrategyPattern
{
    public class EquipmentRelocationSearchResultDTO : SearchResultDTO
    {
        public EquipmentRelocationDto EquipmentRelocationDto { get; internal set; }
    }
}
