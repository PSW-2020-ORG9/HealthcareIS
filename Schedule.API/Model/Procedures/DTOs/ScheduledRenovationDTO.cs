using Schedule.API.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.API.Model.Procedures.DTOs
{
    public class ScheduledRenovationDTO
    {
        public TimeInterval TimeInterval { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
