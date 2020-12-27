using System.ComponentModel.DataAnnotations.Schema;
using General;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Model.Procedures
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
        
        [ForeignKey("ProcedureDetails")]
        public int? ProcedureDetailsId { get; set; }
        public ProcedureDetails ProcedureDetails { get; set; }

        [ForeignKey("ReferredFrom")]
        public int? ReferredFromId { get; set; }
        public Examination ReferredFrom { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }
        
    }
}