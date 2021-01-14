using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.DTOs
{
    public class StepsStatisticsDto
    {
        public int MinSteps { get; set; }
        public int AvgSteps { get; set; }
        public int MaxSteps { get; set; }
    }
}
