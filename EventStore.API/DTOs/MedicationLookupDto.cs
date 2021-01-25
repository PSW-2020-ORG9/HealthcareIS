using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStore.API.DTOs
{
    public class MedicationLookupDto
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
