using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.DTOs
{
    public class SuccessStatisticsDto
    {
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }
}
