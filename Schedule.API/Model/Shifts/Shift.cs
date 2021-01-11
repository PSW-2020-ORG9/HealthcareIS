using System.ComponentModel.DataAnnotations.Schema;
using General;
using Schedule.API.Model.Dependencies;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Model.Shifts
{
    public class Shift : Entity<int>
    {
        public TimeInterval TimeInterval { get; set; }

        [ForeignKey("AssignedExamRoom")]
        public int AssignedExamRoomId { get; set; }
        public Room AssignedExamRoom { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}