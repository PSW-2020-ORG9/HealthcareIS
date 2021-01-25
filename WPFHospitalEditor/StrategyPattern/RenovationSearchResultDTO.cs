using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.StrategyPattern
{
    class RenovationSearchResultDTO : SearchResultDTO
    {
        public RenovationDto RenovationDto { get; internal set; }
    }
}
