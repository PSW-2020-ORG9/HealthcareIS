using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Model
{
    public class ExaminationWithAvailableRescheduling
    {
        public int ExaminationId { get; set; }  
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public ProcedurePriority Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReschedulingDate { get; set; }
        public int RequiredSpecialtyId { get; set; }

        public ExaminationWithAvailableRescheduling(int examinationId, int patientId, int doctorId, ProcedurePriority priority, DateTime startDate, DateTime reschedulingDate, int requiredSpecialtyId)
        {
            ExaminationId = examinationId;
            PatientId = patientId;
            DoctorId = doctorId;
            Priority = priority;
            StartDate = startDate;
            ReschedulingDate = reschedulingDate;
            RequiredSpecialtyId = requiredSpecialtyId;
        }
    }
}
