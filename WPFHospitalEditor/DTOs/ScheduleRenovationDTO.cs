using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.DTOs
{
    public class ScheduleRenovationDTO
    {
        public TimeInterval TimeInterval { get; internal set; }
        public int DoctorId { get; internal set; }
        public int PatientId { get; internal set; }
    }
}
