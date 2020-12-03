using System;
using HealthcareBase.Model.Utilities;

namespace HealthcareBase.Model.Schedule.Procedures.DTOs
{
    public class ScheduledExaminationDTO
    {
        public DateTime StartTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}