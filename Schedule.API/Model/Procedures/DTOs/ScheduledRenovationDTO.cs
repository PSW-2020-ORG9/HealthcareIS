using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.API.Model.Procedures.DTOs
{
    public class ScheduledRenovationDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
