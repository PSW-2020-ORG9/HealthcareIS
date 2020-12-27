using System;

namespace Schedule.API.Model.Procedures.DTOs
{
    public class ScheduledExaminationDTO
    {
        public DateTime StartTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}