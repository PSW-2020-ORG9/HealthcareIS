using System;
using System.ComponentModel.DataAnnotations.Schema;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Procedures
{
    public class ProcedureDetails : Entity<int>
    {
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        
        [Column(TypeName = "nvarchar(12)")]
        public ProcedurePriority Priority { get; set; }

        public int RequiredSpecialtyId { get; set; }
    }
}