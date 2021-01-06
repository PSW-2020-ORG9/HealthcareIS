using System;

namespace WPFHospitalEditor.DTOs
{
    public class ScheduledExaminationDTO
    {
        public DateTime StartTime { get; internal set; }
        public int DoctorId { get; internal set; }
        public int PatientId { get; internal set; }
    }
}
