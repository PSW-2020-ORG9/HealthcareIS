using System;

namespace WPFHospitalEditor.DTOs
{
    public class RoomScheduledAppointmentDto
    {
        public int ExaminationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
