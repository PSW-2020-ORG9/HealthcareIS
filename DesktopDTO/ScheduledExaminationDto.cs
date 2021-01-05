using System;

namespace DesktopDTO
{
    public class ScheduledExaminationDto
    {
        public DateTime StartTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}