using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Model
{
    public abstract class Procedure : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
        public ProcedurePriority Priority { get; set; }
        public int RequiredSpecialtyId { get; set; }
    }
}
