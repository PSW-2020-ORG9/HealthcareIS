using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.DTOs
{
    public class ScheduleRenovationDTO
    {
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }

        public int DoctorId { get; internal set; }
        public int PatientId { get; internal set; }
    }
}
