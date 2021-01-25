using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.DTOs
{
    public class FloorChangeDto
    {
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
    }
}
